<template>
    <div class="content-area">
        <div class="title-content">
            <div class="title">Nhân viên</div>
            <div class="add-new" @click="showFormEmployee">Thêm mới nhân viên</div>
        </div>
        <div :class="['layout-content', isMini? 'layout-content-mini' : '']">
            <div class="top-content">
                <div class="top-button">
                    <div class="grid-button">
                        <div class="delete-more" @click="showPopUpDelete">Xóa nhân viên</div>
                    </div>
                    
                    <div class="filter">
                        <input type="text" class="content-filter" v-model="filter" placeholder="Tìm theo mã, tên nhân viên, số điện thoại" @focus="showToastMessage" v-on:keyup.13="filterEmployee">
                        <div class="mi mi-16 search-icon"></div>
                    </div>
                    <div class="refresh">
                        <div class="mi mi-24 refresh-icon" @click="refresh"></div>
                    </div>
                    
                </div>
                
            </div>
            <div class="grid flex bg-white">
                <div class="grid-view">
                    <BaseTable
                    :listTh="listTh"
                    :datas="listEmployee"
                    ref="tableEmployee"
                    v-on:addEmployeeDelete="addEmployeeDelete"
                    v-on:chooseAllEmployee="chooseAllEmployee"
                    v-on:deleteEmployee="deleteOneEmployee"
                    v-on:editEmployee="editEmployee"
                    v-on:copyEmployee="copyEmployee"
                    />

                </div>
            </div>
            <PageNavigation
            :totalrecord="totalRecord"
            :totalPage="totalPage"
            :currentPage="currentPage"
            :listNumberPage="numberPage"
            v-on:choosePage="choosePage"
            v-on:changePageSize="changePageSize"
            ref="PageNavigation"
            />

            
        </div>
        <TheFormEmployee
        v-if="showForm"
        :employeeData="object"
        :department="departmentName"
        :status="status"
        ref="FormEmployee"
        :listDeparment="listDepartment"
        :listIdDepartment="listIdDepartment"
        :check="checkGender"
        v-on:removeForm="removeForm"
        v-on:showToastError="showToastError"
        v-on:resetForm="resetForm"
        />
        
        <div id="toast"></div>
        <BasePopUp
        :isShow="showPopUp"
        :type="type"
        :message="message"
        :action1="action1"
        :action2="action2"
        :action3="action3"
        :show="optionPopUp"
        v-on:confirmAction="deleteEmployee"
        v-on:cancelAction="stopDelete"
        
        />

        <BaseLoad :load="showLoad"/>
        
    </div>
</template>

<script>
import BasePopUp from '../base/BasePopUp.vue'
import BaseTable from '../base/BaseTable.vue'
import PageNavigation from '../base/PageNavigation.vue'
import Employee from '../../js/api/Employee.js'
import Department from '../../js/api/Department.js'
import BaseLoad from '../base/BaseLoad.vue'
import TheFormEmployee from './TheFormEmployee.vue'


export default {
    name: "ContentEmployee",
    components: {BaseTable, PageNavigation, BasePopUp, BaseLoad, TheFormEmployee},
    props:{
       isMini: Boolean,
    },
    data(){
        return{
            listTh: [
                { content: "MÃ NHÂN VIÊN", class: "th-employee employee-code" },
                { content: "TÊN NHÂN VIÊN", class: "employee-name" },
                { content: "GIỚI TÍNH", class: "employee-gender" },
                { content: "NGÀY SINH", class: "employee-dob" },
                { content: "SỐ CMND", class: "employee-identity" },
                { content: "CHỨC DANH", class: "employee-position" },
                { content: "TÊN ĐƠN VỊ", class: "employee-department" },
                { content: "SỐ TÀI KHOẢN", class: "employee-account" },
                { content: "TÊN NGÂN HÀNG", class: "employee-bank-name" },
                { content: "CHI NHÁNH NGÂN HÀNG", class: "employee-bank-branch" },
                { content: "CHỨC NĂNG", class: "employee-option" },
            ],
            
            pageSize: 20,
            totalRecord : 0,
            totalPage: 5,
            currentPage: 1,
            numberPage: [
                {value: 1, class: "active"},
                {value: 2, class: ""},
                {value: 3, class: ""},
                {value: 5, class: ""},
            ],
            showPopUp: false,
            type: "error",
            message: "Dữ liệu bị thay đổi, bạn có muốn cất không?",
            action1: "Hủy",
            action2: "Không",
            action3: "Có",
            optionPopUp: [false, false],

            listDepartment: [],
            listIdDepartment: [],
            listEmployee: [],
            listIdEmployee: [],

            listIndexDelete: [],

            filter: "",

            showLoad: false,

            showForm : false,

            object: {
                EmployeeId : null,
                EmployeeCode : null,
                EmployeeName : null,
                DateOfBirth : null,
                GenderName : null,
                DepartmentId : null,
                IdentityNumber : null,
                IdentityDate : null,
                IdentityPlace : null,
                EmployeePosition : null,
                Address : null,
                BankAccountNumber : null,
                BankName : null,
                BankBranchName : null,
                PhoneNumber : null,
                TelephoneNumber : null,
                Email : null,
            },

            newEmployeeCode : null,
            departmentName : null,

            status: null, //0: thêm mới, 1: sửa

            checkGender: [true, false, false],

            
        }
    },
    /**
     * Tải dữ liệu
     * create by: TQHA (19/8/2021)
     */
    async created(){
        let me = this;
        let d = {
            PageSize: this.pageSize,
            PageNumber : 0,
        }
        this.showLoad = true;
        await Department.getAll().then(res=>{
            var department = res.data.Data;
            for(var i=0; i<department.length; i++){
                me.listDepartment.push(department[i].DepartmentName);
                me.listIdDepartment.push(department[i].DepartmentId);
            }
        })
        .catch(res=>{
            this.toast({
                action: "Thất bại:",
                content: res.response.data.Message.UserMsg,
                type: "error",
                duration: 3000,
            })
        })
        this.showLoad = false;

        await me.loadData(d);
        
        me.numberPage = []
        if(me.totalPage > 4){
            me.numberPage = [
                {value: 1, class: "active"},
                {value: 2, class: ""},
                {value: 3, class: ""},
                {value: this.totalPage, class: ""},
            ];
        }
        else{
            for(var i=1; i<= me.totalPage; i++){
                if(i == 1){
                    me.numberPage.push({
                        value: 1,
                        class: "active"
                    });
                }
                else{
                    me.numberPage.push({value:i, class: ""});
                }
                
            }
        }
        
    },
    methods:{

        /**
         * Lấy dữ liệu 
         * create by: TQHA (19/8/2021)
         */
        async loadData(d){
            this.showLoad = true;
            let me = this;
            await Employee.filter(d)
            .then(function(res){
                me.listEmployee = res.data.Data;
                me.totalRecord = res.data.Message.UserMsg.TotalRecord;
                me.totalPage = res.data.Message.UserMsg.TotalPage;

                me.listIdEmployee = [];
                for(var i = 0; i<me.listEmployee.length; i++){
                    me.listIdEmployee.push(me.listEmployee[i].EmployeeId);
                    //Lấy tên các phòng ban
                    for(var j=0; j<me.listIdDepartment.length; j++){
                        if(me.listEmployee[i].DepartmentId == me.listIdDepartment[j]){
                            me.listEmployee[i].DepartmentName = me.listDepartment[j];
                            break;
                        }
                    }
                }
                
                
            })
            .catch(function(res){
                this.toast({
                    action: "Thất bại:",
                    content: res.response.data.Message.UserMsg,
                    type: "error",
                    duration: 3000,
                })
            });

            this.showLoad = false;
        },
        /**
         * Lấy mã nhân viên mới
         * create by: TQHA (20/8/2021)
         */
        async getNewCode(){
            this.showLoad = true;
            await Employee.getNewCode()
            .then(res=>{
                this.newEmployeeCode = res.data.Data;
                this.toast({
                    action: "Thành công:",
                    content: "lấy mã nhân viên mới",
                    type: "success",
                    duration: 3000,
                })
            })
            .catch(res=>{
                res
                this.toast({
                    action: "Thất bại:",
                    content: "không thể lấy mã nhân viên mới",
                    type: "error",
                    duration: 3000,
                })
            });
            this.showLoad = false;
            
        },

        /**
         * Chọn trang
         * @param {Number} page: trang hiện tại
         * @param {Array} list: danh sách trang
         * create by: TQHA (19/8/2021)
         */
        choosePage(page, list){
            this.currentPage = page;
            this.numberPage = list;
            
            var d = {
                PageSize: this.pageSize,
                PageNumber : page-1,
                Filter : this.filter,
            }
            this.listIndexDelete = [];
            this.$refs.tableEmployee.unChooseAll();
            this.loadData(d);
            // this.$refs.PageNavigation.setLengthPage(this.pageSize);
        },
        
        /**
         * Khởi tạo toast
         * create by: TQHa (18/7/2021)
         */
        toast({action, content, type, duration }) {
            const main = document.getElementById("toast");
            if (main) {
                const toast = document.createElement("div");
                //auto remove toast
                const autoRemove = setTimeout(function () {
                main.removeChild(toast);
                }, duration + 1000);

                toast.onclick = function (e) {
                if (e.target.closest(".delete-toast")) {
                    main.removeChild(toast);
                    clearTimeout(autoRemove);
                }
                };
                const delay = (duration / 1000).toFixed(2);
                
                toast.classList.add("toast-message");
                toast.style.animation = `slideInLeft ease .5s, fadeOut linear 1s ${delay}s forwards`;
                toast.innerHTML = `
                    <div class="mi mi-16 delete-toast"></div>
                    <p><span class="${type}">${action}&nbsp;</span>${content}</p>
                `;
                main.appendChild(toast);
            }
        },
        /**
         * Thêm nhân viên để xóa
         * create by: TQHA (19/8/2021)
         */
        addEmployeeDelete(index){
            const tmp = this.listIndexDelete.indexOf(index);
            if(tmp == -1){
                this.listIndexDelete.push(index);
            }
            else{
                this.listIndexDelete.splice(tmp, 1);
            }

           
        },
        /**
         * Chọn tất cả nhân viên để xóa
         * create by: TQHA (19/8/2021)
         */
        chooseAllEmployee(status){
            this.listIndexDelete = [];
            if(status == 1){
                for(var i=0; i<this.listEmployee.length; i++){
                    this.listIndexDelete.push(i);
                }
            }

            
        },
        /**
         * Hiển thị popup cảnh báo xóa
         * create by: TQHA (19/8/2021)
         */
        showPopUpDelete(){
            if(this.listIndexDelete.length == 0){
                this.toast({
                    action: "Không thể xóa:",
                    content: "chưa lựa chọn nhân viên",
                    type: "warning",
                    duration: 3000,
                })
            }
            else{
                var message = "";
                if(this.listIndexDelete.length == 1){
                    var code = this.listEmployee[this.listIndexDelete[0]].EmployeeCode;
                    message = `Bạn có thực sự muốn xóa Nhân viên <${code}> không?`;
                }
                else{
                    message = `Bạn có thực sự muốn xóa các Nhân viên đã chọn không?`;
                }
                this.message = message;
                this.type = "warning";
                this.action1 = "Không";
                this.action3 = "Có"
                this.optionPopUp = [true, false];
                this.showPopUp = true;
            }
        },
        /**
         * Xóa một nhân viên
         * create by: TQHA (19/8/2021)
         */
        deleteOneEmployee(index){
            this.listIndexDelete = [];
            this.listIndexDelete.push(index);
            this.showPopUpDelete();
        },
        /**
         * Xóa nhân viên đã chọn
         * create by: TQHA (19/8/2021)
         */
        async deleteEmployee(){
            this.showPopUp = false;
            var listId = [];
            for(var i=0; i<this.listIndexDelete.length; i++){
                listId.push(this.listIdEmployee[this.listIndexDelete[i]]);
            }
            var data = {
                ListId : listId,
            }
            data = JSON.stringify(data);
            var check = false;

            this.showLoad = true;

            await Employee.delete(data)
            .then(res=>{
                res;
                this.toast({
                    action: "Xóa",
                    content: "nhân viên thành công",
                    type: "success",
                    duration: 3000,
                });
                this.numberPage = [
                    {value: "", class: ""},
                    {value: "", class: ""},
                    {value: "", class: ""},
                    {value: "", class: ""}
                ];
                
                check = true;
            })
            .catch(res=>{
                this.toast({
                    action: "Thất bại",
                    content: res.response.data.Message.UserMsg,
                    type: "error",
                    duration: 3000,
                })
            });

            this.showLoad = false;

            if(check){
                
                var d = {
                    PageSize: this.pageSize,
                    PageNumber : 0,
                    Filter: this.filter,
                }
                this.listIndexDelete = [];
                this.$refs.tableEmployee.unChooseAll();
                await this.loadData(d);
                this.resetListPage();
                // this.$refs.PageNavigation.setLengthPage(this.pageSize);
                
            }
        },
        /**
         * Hủy bỏ xóa nhân viên
         * create by: TQHA (19/8/2021)
         */
        stopDelete(){
            this.showPopUp = false;
        },
        /**
         * Hiện thông báo nhấn Enter để lọc
         * create by: TQHA (19/8/2021)
         */
        showToastMessage(){
            this.toast({
                action: "Nhấn Enter",
                content: "để lọc dữ liệu",
                type: "info",
                duration: 3000,
            })
        },

        /**
         * Lọc dữ liệu
         * create by: TQHA (19/8/2021)
         */
        async filterEmployee(){
            
            this.currentPage = 1;
            this.numberPage = [
                {value: "", class: ""},
                {value: "", class: ""},
                {value: "", class: ""},
                {value: "", class: ""}
            ];
            var d = {
                PageSize: this.pageSize,
                PageNumber : 0,
                Filter : this.filter,
            }
            this.listIndexDelete = [];
            this.$refs.tableEmployee.unChooseAll();
            await this.loadData(d);
            this.resetListPage();
            // this.$refs.PageNavigation.setLengthPage(this.pageSize);

        },
        /**
         * Làm mới trang
         * create by: TQHA (19/8/2021)
         */
        async refresh(){
            this.filter = "";
            this.currentPage = 1;
            this.numberPage = [
                {value: "", class: ""},
                {value: "", class: ""},
                {value: "", class: ""},
                {value: "", class: ""}
            ];
            var d = {
                PageSize: this.pageSize,
                PageNumber : 0,
            }
            this.listIndexDelete = [];
            this.$refs.tableEmployee.unChooseAll();
            await this.loadData(d);
            this.resetListPage();
            // this.$refs.PageNavigation.setLengthPage(this.pageSize);
            
            
        },
        /**
         * Thay đổi kích thước trang
         * create by: TQHA (19/8/2021)
         */
        async changePageSize(size){
            this.pageSize = size;
            this.currentPage = 1;
            this.numberPage= [
                {value: "", class: ""},
                {value: "", class: ""},
                {value: "", class: ""},
                {value: "", class: ""},
            ];
            var d = {
                PageSize: this.pageSize,
                PageNumber : 0,
                Filter : this.filter,
            }
            this.listIndexDelete = [];
            this.$refs.tableEmployee.unChooseAll();
            await this.loadData(d);
            this.resetListPage();
        },
        /**
         * Đặt lại thứ tự phân trang
         * create by: TQHA (19/8/2021)
         */
        resetListPage(){
            var page = [];
            
            if(this.totalPage > 4){
                page = [
                    {value: 1, class: "active"},
                    {value: 2, class: ""},
                    {value: 3, class: ""},
                    {value: this.totalPage, class: ""},
                ];
            }
            else{
                for(var i=1; i<= this.totalPage; i++){
                    if(i == 1){
                        page.push({
                            value: 1,
                            class: "active"
                        });
                    }
                    else{
                        page.push({value:i, class: ""});
                    }
                    
                }
            }
            this.numberPage = page;
        },

        /**
         * Hiển thị form điền thông tin nhân viên
         * create by: TQHA (20/8/2021)
         */
        async showFormEmployee(){
            this.showForm = true;
            this.status = 0;
            await this.getNewCode();

            var employeeData = {
                EmployeeId : null,
                EmployeeCode : null,
                EmployeeName : null,
                DateOfBirth : null,
                Gender: 0,
                GenderName : null,
                DepartmentId : null,
                IdentityNumber : null,
                IdentityDate : null,
                IdentityPlace : null,
                EmployeePosition : null,
                Address : null,
                BankAccountNumber : null,
                BankName : null,
                BankBranchName : null,
                PhoneNumber : null,
                TelephoneNumber : null,
                Email : null,
                CreatedDate : null,
                CreatedBy : null,
                ModifiedDate : null,
                ModifiedBy : null,
            };
            employeeData.EmployeeCode = this.newEmployeeCode;
           
            this.object = employeeData;
        },
        /**
         * Chỉnh sửa thông tin nhân viên
         * create by: TQHA (20/8/2021)
         * 
         */
        editEmployee(index){
            this.object = this.listEmployee[index];
            this.object.DateOfBirth = this.formatDate(this.object.DateOfBirth);
            this.object.IdentityDate = this.formatDate(this.object.IdentityDate);
            
            for(var i=0; i<this.listIdDepartment.length; i++){
                if(this.object.DepartmentId == this.listIdDepartment[i]){
                    this.departmentName = this.listDepartment[i];
                    break; 
                }
            }
            switch(this.object.Gender){
                case 0: this.checkGender = [true, false, false];
                        break;
                case 1 : this.checkGender = [false, true, false];
                        break;
                default: this.checkGender = [false, false, true];
                        break;
            }
            this.status = 1;
            this.showForm = true;

        },
        /**
         * Nhân bản thông tin nhân viên
         * create by: TQHA (20/8/2021)
         */
        async copyEmployee(index){
            this.object =  Object.assign({}, this.listEmployee[index]);
            this.object.DateOfBirth = this.formatDate(this.object.DateOfBirth);
            this.object.IdentityDate = this.formatDate(this.object.IdentityDate);
            await this.getNewCode();

            for(var i=0; i<this.listIdDepartment.length; i++){
                if(this.object.DepartmentId == this.listIdDepartment[i]){
                    this.departmentName = this.listDepartment[i];
                    break; 
                }
            }
            switch(this.object.Gender){
                case 0: this.checkGender = [true, false, false];
                        break;
                case 1 : this.checkGender = [false, true, false];
                        break;
                default: this.checkGender = [false, false, true];
                        break;
            }
            this.object.EmployeeCode = this.newEmployeeCode;

            this.showForm = true;
            this.status = 0;

        },

        /**
         * Định dạng lại ngày theo D/MM/YYYY
         * create by: TQHA (19/8/2021)
         */
        formatDate(date) {
            if (date == "" || date == null) {
                return "";
            }
            var dat = new Date(date);
            
            return dat;
        },
        
        /**
         * Hủy form đang nhập
         * create by: TQHA (20/8/2021)
         */
        async removeForm(status){
            this.object = {
                EmployeeId : null,
                EmployeeCode : null,
                EmployeeName : null,
                DateOfBirth : null,
                GenderName : null,
                DepartmentId : null,
                IdentityNumber : null,
                IdentityDate : null,
                IdentityPlace : null,
                EmployeePosition : null,
                Address : null,
                BankAccountNumber : null,
                BankName : null,
                BankBranchName : null,
                PhoneNumber : null,
                TelephoneNumber : null,
                Email : null,
                CreatedDate : null,
                CreatedBy : null,
                ModifiedDate : null,
                ModifiedBy : null,
            }
            this.departmentName = null;
            this.checkGender = [true, false, false];
            this.showForm = false;
            this.listIndexDelete = [];
            this.$refs.tableEmployee.unChooseAll();
            // this.$refs.PageNavigation.setLengthPage(this.pageSize);
            if(status == 1){
                   
                var d = {
                    PageSize: this.pageSize,
                    PageNumber : 0,
                    Filter : this.filter,
                }
                 
                await this.loadData(d);
                this.resetListPage();
                
            }
            else if(status == 2){
                d = {
                    PageSize: this.pageSize,
                    PageNumber : this.currentPage - 1,
                    Filter : this.filter,
                }
                await this.loadData(d);
            }

        },

        async resetForm(status){
            await this.getNewCode();
            this.object = {
                EmployeeId : null,
                EmployeeCode : this.newEmployeeCode,
                EmployeeName : null,
                DateOfBirth : null,
                Gender: 0,
                DepartmentId : null,
                IdentityNumber : null,
                IdentityDate : null,
                IdentityPlace : null,
                EmployeePosition : null,
                Address : null,
                BankAccountNumber : null,
                BankName : null,
                BankBranchName : null,
                PhoneNumber : null,
                TelephoneNumber : null,
                Email : null,
                CreatedDate : null,
                CreatedBy : null,
                ModifiedDate : null,
                ModifiedBy : null,
            }
            this.status = 0;
            this.checkGender = [true, false, false];
            this.departmentName = null;
            this.listIndexDelete = [];
            this.$refs.tableEmployee.unChooseAll();
            // this.$refs.PageNavigation.setLengthPage(this.pageSize);
            if(status == 1){
                   
                var d = {
                    PageSize: this.pageSize,
                    PageNumber : 0,
                    Filter : this.filter,
                }
                 
                await this.loadData(d);
            }
            else if(status == 2){
                d = {
                    PageSize: this.pageSize,
                    PageNumber : this.currentPage - 1,
                    Filter : this.filter,
                }
                await this.loadData(d);
            }
        },

        /**
         * Thực hiện hành động thất bại
         * create by: TQHA (21/8/2021)
         */
        showToastError(message){
            this.toast({
                action: "Thất bại",
                content: message,
                type: "error",
                duration: 3000,
            });
        },
    }
}
</script>

<style>
    @import url(../../css/common/toast.css);
    @import url(../../css/content/employee.css);
   
</style>