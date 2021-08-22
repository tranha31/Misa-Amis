<template>
<div>
    <form class="contructor" id="form-employee">
        <div class="background">

        </div>
        <div class="main-form">
            <div class="form-header">
                <div class="form-header-option">
                    <div class="form-title">Thông tin nhân viên</div>
                    <div class="form-option">
                        <div @focus="focusEmployee" tabindex="0"></div>
                        <input type="checkbox" id="checkbox-em" class="checkbox">
                        
                        <label for="checkbox-em">
                            <div class="mi mi-16 form-checkbox"></div>
                            <div class="label-content">Là khách hàng</div>
                        </label>
                    </div>
                    <div class="form-option">
                        <input type="checkbox" id="checkbox-provide" class="checkbox">
                        <label for="checkbox-provide">
                            <div class="mi mi-16 form-checkbox"></div>
                            <div class="label-content">Là nhà cung cấp</div>
                        </label>
                    </div>
                    
                </div>
                <div class="close-form">
                    <div class="mi mi-24 close" v-tooltip.bottom="'Đóng form'" @click="hiddenForm"></div>
                </div>
            </div>
            <div class="form-content">
                <div class="form-content-1 h1-2">
                    <div class="form-content-left w1-2">
                        <div class="grid-form">
                            <BaseInput
                            class="w2-5 p-6"
                            title="Mã"
                            required="*"
                            maxL="20"
                            notEmpty
                            employeeCode
                            :value="employeeData.EmployeeCode"
                            ref="EmployeeCode"
                            />

                            <BaseInput
                            class="w3-5"
                            title="Tên"
                            required="*"
                            maxL="100"
                            notEmpty
                            employeeName
                            :value="employeeData.EmployeeName"
                            ref="EmployeeName"
                            />
                        </div>
                        <div class="title">Đơn vị&nbsp;<span>*</span></div>
                        <div class="combobox" id="combobox" v-tooltip.bottom="messageDeparment" ref="DepartmentName">
                            
                            <v-autocomplete 
                            id="cbb" 
                            :items="items" 
                            v-model="departmentName" 
                            :get-label="getLabel" 
                            :component-item='template' 
                            @update-items="updateItems"
                            @input="inputCBB"
                            @blur="blurCBB"
                            @focus="focusCombobox"
                            @item-clicked="removeMessage"
                            @item-selected="removeMessage"
                            @change="changeContentCBB"
                            >
                            </v-autocomplete>

                            <label for="cbb" class="btn-cbb" @click="focusCbb" v-click-outside="removeClass">
                                <div class="mi mi-16 arrow"></div>
                            </label>
                        </div>
                        <div class="input">
                            <BaseInput
                            class="w-full"
                            title="Chức danh"
                            maxL="255"
                            :value="employeeData.EmployeePosition"
                            ref="EmployeePosition"/>
                        </div>
                        
                        
                    </div>
                    <div class="form-content-right w1-2">
                        <div class="grid-form">
                            <div class="date" style="margin-right: 6px" v-tooltip.bottom="messageDOB">
                                <div class="title" style="margin-top: 1px">Ngày sinh</div>
                                <date-picker id="date-of-birth"
                                    v-model="dateOfBirth"
                                    format="DD/MM/YYYY"
                                    type="date"
                                    placeholder = "DD/MM/YYYY"
                                    style="width:156px"
                                    ref="DateOfBirth"
                                    @input="inputDateOfBirth"
                                    
                                    @clear="clearData(0)"
                                ></date-picker>
                            </div>
                            <div class="gender">
                                <div class="title" >Giới tính</div>
                                <div class="gender-option">
                                    <input type="radio" id="male" class="radio" name="gender" :checked="checkGender[0]">
                                    
                                    <label for="male">
                                        <div class="mi mi-16 gender-radio"></div>
                                        <div class="label-content">Nam</div>
                                    </label>

                                    <input type="radio" id="felmale" class="radio" name="gender" :checked="checkGender[1]">
                                    
                                    <label for="felmale">
                                        <div class="mi mi-16 gender-radio"></div>
                                        <div class="label-content">Nữ</div>
                                    </label>

                                    <input type="radio" id="orther" class="radio" name="gender" :checked="checkGender[2]">
                                    
                                    <label for="orther">
                                        <div class="mi mi-16 gender-radio"></div>
                                        <div class="label-content">Khác</div>
                                    </label>
                                </div>
                            </div>

                        </div>

                        <div class="grid-form">
                            <BaseInput
                            title="Số CMND"
                            class="w3-5"
                            maxL="20"
                            ref="IdentityNumber"
                            isNumber
                            identity
                            :value="employeeData.IdentityNumber"
                            />

                            <div class="date" style="margin-left: 6px" v-tooltip.bottom="messageIdate">
                                <div class="title" style="margin-top: 1px" >Ngày cấp</div>
                                <date-picker id="identity-date"
                                    v-model="identityDate"
                                    format="DD/MM/YYYY"
                                    type="date"
                                    placeholder = "DD/MM/YYYY"
                                    style="width:156px"
                                    @input="inputIdentityDate"
                                    @clear="clearData(1)"
                                    ref="IdentityDate"
                                ></date-picker>
                            </div>
                            
                            
                        </div>

                        <BaseInput
                        title="Nơi cấp"
                        class="w-full"
                        maxL="255"
                        :value="employeeData.IdentityPlace"
                        ref="IdentityPlace"
                        />
                    </div>
                </div>
                <div class="form-content-2 h1-2">
                    <BaseInput
                    title="Địa chỉ"
                    class="w-full"
                    maxL="255"
                    :value="employeeData.Address"
                    ref="Address"/>
                    <div class="grid-form" style="margin-top:12px">
                        <BaseInput
                        title="ĐT di động"
                        maxL="20"
                        class="p-r-6"
                        ref="PhoneNumber"
                        phoneNumber
                        :value="employeeData.PhoneNumber"
                        />

                        <BaseInput
                        title="ĐT cố định"
                        maxL="20"
                        class="p-r-6"
                        telephoneNumber
                        :value="employeeData.TelephoneNumber"
                        ref="TelephoneNumber"
                        />

                        <BaseInput
                        title="Email"
                        maxL="50"
                        class="p-r-6"
                        ref="Email"
                        email
                        :value="employeeData.Email"
                        />
                    </div>

                    <div class="grid-form" style="margin-top:12px">
                        <BaseInput
                        title="Tài khoản ngân hàng"
                        maxL="20"
                        class="p-r-6"
                        ref="BankAccountNumber"
                        isNumber
                        :value="employeeData.BankAccountNumber"
                        />

                        <BaseInput
                        title="Tên ngân hàng"
                        maxL="255"
                        class="p-r-6"
                        ref="BankName"
                        :value="employeeData.BankName"
                        />

                        <BaseInput
                        title="Chi nhánh"
                        maxL="255"
                        class="p-r-6"
                        ref="BankBranchName"
                        :value="employeeData.BankBranchName"
                        />

                        <div @focus="focusEmployee" tabindex="0"></div>
                    </div>
                </div>

                <div class="footer-form">
                    <div class="divide"></div>
                    <div class="button-footer">
                        <div class="btn-cancel">
                            <div class="btn cancel" v-tooltip.bottom="'Hủy form'" @click="removeForm(0)">Hủy</div>
                        </div>
                        
                        <div class="btn save" v-tooltip.bottom="'Lưu đối tượng'" @click="sendData(0)">Cất</div>
                        <div class="btn save-add" v-tooltip.bottom="'Lưu đối tượng và tạo mới nội dung form'" @click="sendData(1)">Cất và thêm</div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <BasePopUp
    :isShow="showPopUp"
    :type="type"
    :message="message"
    :action1="action1"
    :action2="action2"
    :action3="action3"
    :show="optionPopUp"
    v-on:confirmAction="confirmAction"
    v-on:cancelAction="cancelAction"
    v-on:confirmAction2="confirmAction2"
    />

    <BaseLoad :load="showLoad"/>
</div>
</template>

<script>
import BaseInput from '../base/BaseInput.vue'
import ItemTemplate from '../base/ItemTemplate.vue'
import BasePopUp from '../base/BasePopUp.vue'
import DatePicker from 'vue2-datepicker';
import 'vue2-datepicker/index.css';
import EmployeeForm from '../../js/entity/EmployeeForm.js';
import Employee from '../../js/api/Employee.js'
import EmployeeFrom from '../../js/entity/EmployeeForm.js';
import BaseLoad from '../base/BaseLoad.vue';

export default {
    name: "FormEmployee",
    components: { BaseInput, DatePicker, BasePopUp, BaseLoad },
    props:{
        employeeData: Object,
        department: String,
        listDeparment: Array,
        listIdDepartment: Array,
        status: Number,
        check: Array,
        
    },
    data() {
        return {
            departmentName: this.department,
            items: this.listDeparment,
            template: ItemTemplate,

            dateOfBirth: this.employeeData.DateOfBirth,
            identityDate: this.employeeData.IdentityDate,

            values : this.employeeData,

            checkGender : this.check,
            
            showPopUp : false,
            type : "warning",
            message : "",
            action1: "Hủy",
            action2: "Không",
            action3: "Có",
            optionPopUp: [true, true],

            messageDOB: "",
            messageIdate: "",
            messageDeparment : "",

            typeForm : this.status,
            showLoad : false,

            refValue : "",
        }
    },
    mounted(){
        this.$refs.EmployeeCode.focusInput();
    },

    watch:{
        department: function () {
            this.departmentName = this.department;
        },
        employeeData: function(){
            this.dateOfBirth = this.employeeData.DateOfBirth;
            this.identityDate = this.employeeData.IdentityDate
        },

        listDeparment: function(){
            this.items = this.listDeparment;
        },

        check: function(){
            this.checkGender = this.check;
        },

        status: function(){
            this.typeForm = this.status;
        }
    },

    methods:{
        

        /**
         * Chọn item cho cbb
         * create by: TQHA (20/8/2021)
         */
        getLabel (item) {
            return item
        },

        updateItems (text) {
            if(text == null || text == ""){
                this.items = this.listDeparment;
            }
            else{
                var tmp = [];
                for(var i=0; i<this.listDeparment.length; i++){
                    if (this.listDeparment[i].includes(text)) {
                        tmp.push(this.listDeparment[i]);
                    }
                }
                this.items = tmp;
                
            }
            
        },
        /**
         * Sự kiện nhập combobox
         * create by: TQHa (21/8/2021)
         */
        inputCBB(){
            if(this.departmentName == null || this.departmentName == ""){
                this.items = this.listDeparment;
            }
            
        },
        /**
         * Bắt sự kiện focus CBB
         * create by: TQHA (22/8/2021)
         */
        focusCombobox(){
            setTimeout(function(){
                var label = document.getElementById("combobox").querySelector(".btn-cbb");
                label.classList.add("btn-cbb-active");
            },100)
            
        },
        /**
         * Bắt sự kiện thay đổi dữ liệu của cbb
         * create by: TQHA (22/8/2021)
         */
        changeContentCBB(){
            setTimeout(function(){
                var tmp = document.getElementById("cbb").querySelectorAll(".item-cbb");
                tmp.forEach(element=>{
                    element.classList.remove("item-choose");
                })
            }, 10)
        },
        /**
         * Sự kiện khi nhấn ra ngoài cbb
         * create by: TQHA (21/8/2021)
         */
        blurCBB(){
            
            var em = new EmployeeForm();
            var de = document.getElementById("combobox");
            var input = de.querySelector("input");
            
            if(input.value == null || input.value == ""){
                
                de.classList.add("error");
                this.messageDeparment = em.DepartmentNameEmpty;
            }
            else{
                var check = false;
                for(var i=0; i<this.listDeparment.length; i++){
                    if(input.value == this.listDeparment[i]){
                        check = true;
                        break;
                    }
                }
                if(!check){
                    de.classList.add("error");
                    this.messageDeparment = em.InvalidDepartment;
                }
                else{
                    de.classList.remove("error");
                    this.messageDeparment = "";
                }
            }
              
        },

        /**
         * Hiển thị list option
         * create by: TQHA (20/8/2021)
         */
        focusCbb(){
            var cbb = document.getElementById("cbb");
            var input = cbb.querySelector("input");
            
            var label = document.getElementById("combobox").querySelector(".btn-cbb");
            if(label.classList.contains("btn-cbb-active")){
                input.blur();
                label.classList.remove("btn-cbb-active");
            }
            else{
                input.focus();
                label.classList.add("btn-cbb-active");
                this.items = this.listDeparment;
                setTimeout(function(){
                    var tmp = document.getElementById("cbb").querySelectorAll(".item-cbb");
                    tmp.forEach(element=>{
                        if(element.querySelector("span").innerHTML == input.value){
                            element.classList.add("item-choose");
                        }
                        else{
                            element.classList.remove("item-choose");
                        }
                        
                    })
                }, 10)
                
            }
            
        },
        /**
         * xóa thông báo của cbb
         * create by: TQHA (21/8/2021)
         */
        removeMessage(){
            var de = document.getElementById("combobox");
            if(de != null){
                de.classList.remove("error");
                this.messageDeparment = "";
            }
            this.items = this.listDeparment;
            
        },
        /**
         * Bỏ xoay ở option khi nhấn ra ngoài
         * create by: TQHA (20/8/2021)
         */
        removeClass(){

            var label = document.getElementById("combobox").querySelector(".btn-cbb");
            label.classList.remove("btn-cbb-active");
 
        },
        /**
         * Huỷ bỏ form
         * create by: TQHA (20/8/2021)
         */
        removeForm(status){
            this.departmentName = null;
            this.dateOfBirth = null;
            this.identityDate = null;
            var input = document.getElementById("form-employee").querySelectorAll("input");
            input.forEach(element=>{
                element.value = null;
            });
            this.checkGender = [true, false, false];
            this.$emit("removeForm", status);
        },
        /**
         * Làm mới nội dung form
         * create by: TQHA (21/8/2021)
         */
        resetForm(status){
            this.departmentName = null;
            this.dateOfBirth = null;
            this.identityDate = null;
            this.checkGender = [true, false, false];
            
            var input = document.getElementById("form-employee").querySelectorAll("input");
            input.forEach(element=>{
                element.value = null;
            });

            this.$emit("resetForm", status);
        },
        /**
         * Đóng form
         * create by: TQHA (21/8/2021)
         */
        hiddenForm(){
            const form = document.getElementById("form-employee");
            
            var gender;
            if(form[7].checked){
                gender = 0;
            }
            else if(form[8].checked){
                gender = 1;
            }
            else if(form[9].checked){
                gender = 2;
            }

            //Lấy dữ liệu để check

            var employeeCode = form[2].value ? form[2].value : null;
            var employeeName = form[3].value ? form[3].value : null;
            var deparment1 = form[4].value ? form[4].value : null;
            var employeePosition = form[5].value ? form[5].value : null;
            var dateOfBirth = form[6].value ? form[6].value : null;
            var identityNumber = form[10].value ? form[10].value : null;
            var iDate = form[11].value ? form[11].value : null;
            var identityPlace = form[12].value ? form[12].value : null;
            var address = form[13].value ? form[13].value : null;
            var phoneNumber = form[14].value ? form[14].value : null;
            var telephone = form[15].value ? form[15].value : null;
            var email = form[16].value ? form[16].value : null;
            var bankAccount = form[17].value ? form[17].value : null;
            var bankName = form[18].value ? form[18].value : null;
            var bankBranch = form[19].value ? form[19].value : null;
            
            var onchange = false;

            //Kiểm tra thay đổi dữ liệu
            if(
                employeeCode != this.employeeData.EmployeeCode ||
                employeeName != this.employeeData.EmployeeName ||
                deparment1 != this.departmentName ||
                employeePosition != this.employeeData.EmployeePosition ||
                dateOfBirth != this.formatDate(this.employeeData.DateOfBirth) ||
                identityNumber != this.employeeData.IdentityNumber ||
                iDate != this.formatDate(this.employeeData.IdentityDate) ||
                identityPlace != this.employeeData.IdentityPlace ||
                address != this.employeeData.Address ||
                phoneNumber != this.employeeData.PhoneNumber ||
                telephone != this.employeeData.TelephoneNumber ||
                email != this.employeeData.Email ||
                bankAccount != this.employeeData.BankAccountNumber ||
                bankName != this.employeeData.BankName ||
                bankBranch != this.employeeData.BankBranchName ||
                gender != this.employeeData.Gender
            ){
                onchange = true;
            }
            //Nếu có hiển thị thông báo
            
            if(onchange){
                var em = new EmployeeForm();
                this.showPopUp = true;
                this.type = "info";
                this.optionPopUp = [true, true];
                this.action1 = "Hủy";
                this.action2 = "Không";
                this.action3 = "Có";
                this.message = em.ChangeData;
            }
            //Không thì đóng form
            else{
                this.removeForm(0);
            }

            
        },

        /**
         * Định dạng lại ngày theo D/MM/YYYY
         * create by: TQHA (19/8/2021)
         */
        formatDate(date) {
            if (date == "" || date == null) {
                return null;
            }
            var dat = new Date(date);
            var day = dat.getDate();
            var month = dat.getMonth() + 1;
            var year = dat.getFullYear();
            if (day < 10) {
                day = "0" + day;
            }
            if (month < 10) {
                month = "0" + month;
            }
            return day + "/" + month + "/" + year;
        },

        /**
         * Xác nhận hành động chính của popup
         * create by: TQHA (21/8/2021)
         */
        confirmAction(){
            if(this.optionPopUp[0] == false && this.optionPopUp[1]==false){
                this.showPopUp = false;
                if(this.refValue != ""){
                    if(this.refValue == "DepartmentName"){
                        document.getElementById("combobox").querySelector("input").focus();
                    }
                    else if(this.refValue == "DateOfBirth"){
                        document.getElementById("date-of-birth").querySelector("input").focus();
                    }
                    else if(this.refValue == "IdentityDate"){
                        document.getElementById("identity-date").querySelector("input").focus();
                    }
                    else{
                        this.$refs[this.refValue].focusInput();
                    }
                }
                
            }
            else{
                this.showPopUp = false;
                this.sendData(0);
            }
        },
        /**
         * Dừng hành động của popup
         * create by: TQHA (21/8/2021)
         */
        cancelAction(){
            this.showPopUp = false;
        },
        /**
         * Xác nhận hành động phụ của popup
         * create by: TQHa (21/8/2021)
         */
        confirmAction2(){
            this.showPopUp = false;
            this.removeForm(0);
        },
        
        inputDateOfBirth(){
            var em = new EmployeeForm();
            var date = document.getElementById("date-of-birth");
            var input = date.querySelector(".mx-input");
            if(input.value != null){
                if(!this.checkAge(input.value)){
                    input.classList.add("error");
                    this.messageDOB = em.InvalidDOB;
                }
                else{
                    input.classList.remove("error");
                    this.messageDOB = "";
                }
            }
            else{
                input.classList.remove("error");
                this.messageDOB = "";
            }
            
            
        },

        inputIdentityDate(){
            var em = new EmployeeForm();
            var date = document.getElementById("identity-date");
            var input = date.querySelector(".mx-input");

            var idate = document.getElementById("date-of-birth").querySelector(".mx-input");
            if(input.value != null){
                if(idate.value != null){
                    if(!this.checkDateOfIdentity(idate.value, input.value)){
                        input.classList.add("error");
                        this.messageIdate = em.InvalidIDate;
                    }
                    else{
                        input.classList.remove("error");
                        this.messageIdate = "";
                    }
                }
            }
            else{
                input.classList.remove("error");
                this.messageIdate = "";
            }
        },

        /**
         * Xóa dữ liệu date
         * create by: TQHA (21/8/2021)
         */
        clearData(type){
            if(type == 0){
                var date = document.getElementById("date-of-birth");
                var input = date.querySelector(".mx-input");
                input.classList.remove("error");
                this.messageDOB = "";
            }
            else{
                date = document.getElementById("identity-date");
                input = date.querySelector(".mx-input");
                input.classList.remove("error");
                this.messageIdate = "";
            }
        },
        
        /**
         * Kiểm tra dữ liệu trước khi gửi đi
         * create by: TQHA (21/8/2021)
         */
        checkData(){
            const form = document.getElementById("form-employee");

            //Lấy dữ liệu để check

            var employeeCode = form[2].value ? form[2].value : null;
            var employeeName = form[3].value ? form[3].value : null;
            var deparment1 = form[4].value ? form[4].value : null;
            var dateOfBirth = form[6].value ? form[6].value : null;
            var identityNumber = form[10].value ? form[10].value : null;
            var iDate = form[11].value ? form[11].value : null;
            var phoneNumber = form[14].value ? form[14].value : null;
            var telephone = form[15].value ? form[15].value : null;
            var email = form[16].value ? form[16].value : null;
            
            
            var em = new EmployeeForm();
            var isError = false;
            var messageError = "";

            //Kiểm tra mã nhân viên
            if(employeeCode == null){
                this.refValue = "EmployeeCode";
                this.$refs.EmployeeCode.setClassError();
                this.$refs.EmployeeCode.setValueMessage(em.EmployeeCodeEmpty);
                messageError = em.EmployeeCodeEmpty;
                isError = true;
            }
            else if (!this.$refs.EmployeeCode.checkEmployeeCode(employeeCode)){
                this.refValue = "EmployeeCode";
                this.$refs.EmployeeCode.setClassError();
                this.$refs.EmployeeCode.setValueMessage(em.InvalidCode);
                messageError = em.InvalidCode;
                isError = true;
            }

            //Kiểm tra tên nhân viên
            if(employeeName == null){
                if(!isError){
                    this.refValue = "EmployeeName";
                    isError = true;
                    messageError = em.EmployeeNameEmpty;
                }
                
                this.$refs.EmployeeName.setClassError();
                this.$refs.EmployeeName.setValueMessage(em.EmployeeNameEmpty);
                
            }
            else if(!this.$refs.EmployeeName.checkFullName(employeeName)){
                if(!isError){
                    this.refValue = "EmployeeName";
                    isError = true;
                    messageError = em.InvalidName;
                }
                
                this.$refs.EmployeeName.setClassError();
                this.$refs.EmployeeName.setValueMessage(em.InvalidName);
                
            }

            //Kiểm tra đơn vị
            if(deparment1 == null){
                if(!isError){
                    this.refValue = "DepartmentName";
                    isError = true;
                    messageError = em.DepartmentNameEmpty;
                }
                
                document.getElementById("combobox").classList.add("error");
                this.messageDeparment = em.DepartmentNameEmpty;
            }
            else{
                var check = false;
                for(var i=0; i<this.listDeparment.length; i++){
                    if(deparment1 == this.listDeparment[i]){
                        check = true;
                        break;
                    }
                }
                if(!check){
                    if(!isError){
                        this.refValue = "DepartmentName";
                        isError = true;
                        messageError = em.InvalidDepartment;
                    }
                    
                    document.getElementById("combobox").classList.add("error");
                    this.messageDeparment = em.InvalidDepartment;
                }
            }

            //Kiểm tra tuổi nhân viên
            if(dateOfBirth != null){
                if(!this.checkAge(dateOfBirth)){
                    var date = document.getElementById("date-of-birth");
                    var input = date.querySelector(".mx-input");
                    if(!isError){
                        isError = true;
                        this.refValue = "DateOfBirth";
                        messageError = em.InvalidDOB;
                    }
                    
                    input.classList.add("error");
                    this.messageDOB = em.InvalidDOB;
                    
                }
            }

            //Kiểm tra số cccd/cmnd có hợp lệ
            if(identityNumber != null){
                if(!this.$refs.IdentityNumber.checkIndentityCode(identityNumber)){
                    if(!isError){
                        isError = true;
                        messageError = em.InvalidIdentity;
                        this.refValue = "IdentityNumber";
                    }
                    
                    this.$refs.IdentityNumber.setClassError();
                    this.$refs.IdentityNumber.setValueMessage(em.InvalidIdentity);
                }
            }

            //Kiểm tra số điện thoại di động
            if(phoneNumber != null){
                if(!this.$refs.PhoneNumber.checkPhoneNumber(phoneNumber)){
                    if(!isError){
                        isError = true;
                        messageError = em.InvalidPhone;
                        this.refValue = "PhoneNumber";
                    }
                    
                    this.$refs.PhoneNumber.setClassError();
                    this.$refs.PhoneNumber.setValueMessage(em.InvalidPhone);
                }
            }

            //Kiểm tra số điện thoại cố định
            if(telephone != null){
                if(!this.$refs.TelephoneNumber.checkTelePhone(telephone)){
                    if(!isError){
                        isError = true;
                        messageError = em.InvalidTelePhone;
                        this.refValue = "TelephoneNumber";
                    }
                    
                    this.$refs.TelephoneNumber.setClassError();
                    this.$refs.TelephoneNumber.setValueMessage(em.InvalidTelePhone);
                }
            }

            //Kiểm tra định dạng email
            if(email != null){
                if(!this.$refs.Email.checkEmail(email)){
                    if(!isError){
                        isError = true;
                        messageError = em.InvalidEmail;
                        this.refValue = "Email";
                    }
                    
                    
                    this.$refs.Email.setClassError();
                    this.$refs.Email.setValueMessage(em.InvalidEmail);
                }
            }

            //Kiểm tra ngày cấp cccd/cmnd
            if(iDate != null && dateOfBirth != null){
                if(!this.checkDateOfIdentity(dateOfBirth, iDate)){
                    date = document.getElementById("identity-date");
                    input = date.querySelector(".mx-input");
                    if(!isError){
                        isError = true;
                        messageError = em.InvalidIDate;
                        this.refValue = "IdentityDate";
                    }
            
                    
                    input.classList.add("error");
                    this.messageDOB = em.InvalidIDate;
                    
                }
            }

            return isError ? [false, messageError] : [true, ""];
        },

        /**
         * Gửi dữ liệu
         * @param {Number} type : type = 0 : gửi rồi đóng form, type = 1: gửi rồi làm mới form
         * create by: TQHA (21/8/2021)
         */
        async sendData(type){
            var result = this.checkData();
            if(!result[0]){
                this.showPopUp = true;
                this.type = "error";
                this.optionPopUp = [false, false];
                this.action3 = "Có";
                this.message = result[1];
            }

            else{
                this.refValue = "";
                await this.callApi(type);
            }
            
        },
        /**
         * Gọi api để add hoặc update
         * @param {Number} type : type = 0 : gửi rồi đóng form, type = 1: gửi rồi làm mới form
         * create by: TQHA(21/8/2021)
         */
        async callApi(type){
            var em = new EmployeeFrom();
            const form = document.getElementById("form-employee");
            
            var gender;
            if(form[7].checked){
                gender = 0;
            }
            else if(form[8].checked){
                gender = 1;
            }
            else if(form[9].checked){
                gender = 2;
            }

            var deName = document.getElementById("combobox").querySelector("input").value;
            var departmentId;
            for(var i=0; i<this.listDeparment.length; i++){
                if(deName == this.listDeparment[i]){
                    departmentId = this.listIdDepartment[i];
                    break;
                }
            }
            //Lấy dữ liệu để gửi

            var employeeCode = form[2].value;
            var employeeName = form[3].value; 
            var employeePosition = form[5].value ? form[5].value : null;
            var dateOfBirth = form[6].value ? form[6].value : null;
            var tmpDateOfBirth;
            if(dateOfBirth != null){
                tmpDateOfBirth = dateOfBirth.substring(6) + '-' + dateOfBirth.substring(3,5) + '-' + dateOfBirth.substring(0,2);
            }

            var identityNumber = form[10].value ? form[10].value : null;
            var iDate = form[11].value ? form[11].value : null;
            var tmpIdentityDate;
            if(iDate != null){
                tmpIdentityDate = iDate.substring(6) + '-' + iDate.substring(3,5) + '-' + iDate.substring(0,2);
            }

            var identityPlace = form[12].value ? form[12].value : null;
            var address = form[13].value ? form[13].value : null;
            var phoneNumber = form[14].value ? form[14].value : null;
            var telephone = form[15].value ? form[15].value : null;
            var email = form[16].value ? form[16].value : null;
            var bankAccount = form[17].value ? form[17].value : null;
            var bankName = form[18].value ? form[18].value : null;
            var bankBranch = form[19].value ? form[19].value : null;

            var d1 = {
                EmployeeCode : employeeCode,
                EmployeeName : employeeName,
                DateOfBirth : tmpDateOfBirth,
                Gender: gender,
                DepartmentId : departmentId,
                IdentityNumber : identityNumber,
                IdentityDate : tmpIdentityDate,
                IdentityPlace : identityPlace,
                EmployeePosition : employeePosition,
                Address : address,
                BankAccountNumber : bankAccount,
                BankName : bankName,
                BankBranchName : bankBranch,
                PhoneNumber : phoneNumber,
                TelephoneNumber : telephone,
                Email : email,
            }
            d1 = JSON.stringify(d1);

            var d2 = {
                EmployeeId : this.employeeData.EmployeeId,
                EmployeeCode : employeeCode,
                EmployeeName : employeeName,
                DateOfBirth : tmpDateOfBirth,
                Gender: gender,
                DepartmentId : departmentId,
                IdentityNumber : identityNumber,
                IdentityDate : tmpIdentityDate,
                IdentityPlace : identityPlace,
                EmployeePosition : employeePosition,
                Address : address,
                BankAccountNumber : bankAccount,
                BankName : bankName,
                BankBranchName : bankBranch,
                PhoneNumber : phoneNumber,
                TelephoneNumber : telephone,
                Email : email,
            }

            d2 = JSON.stringify(d2);

            if(type == 0){
                //Nếu thêm mới
                if(this.typeForm == 0){
                    var me = this;
                    this.showLoad = true;
                    await Employee.addNew(d1)
                    .then(res=>{
                        me.showLoad = false;
                        if(res.data.Message.Code == 400){
                            var r = res.data.Message.DevMsg.substring(14);
                            var mess = res.data.Message.UserMsg.substring(21) + " " + em.Exist;
                            me.type = "warning";
                            me.showPopUp = true;
                            me.optionPopUp = [false, false];
                            me.action1 = "";
                            me.action2 = "";
                            me.action3 = "Đồng ý";
                            me.message = mess;

                            if(r == "EmployeeCode"){
                                this.refValue = "EmployeeCode";
                                me.$refs.EmployeeCode.setClassError();
                                me.$refs.EmployeeCode.setValueMessage(em.ExistValue);
                            }
                            else if(r == "IdentityNumber"){
                                this.refValue = "IdentityNumber";
                                me.$refs.IdentityNumber.setClassError();
                                me.$refs.IdentityNumber.setValueMessage(em.ExistValue);
                            }
                            else if(r == "BankAccountNumber"){
                                this.refValue = "BankAccountNumber";
                                me.$refs.BankAccountNumber.setClassError();
                                me.$refs.BankAccountNumber.setValueMessage(em.ExistValue);
                            }
                            else if(r == "PhoneNumber"){
                                this.refValue = "PhoneNumber";
                                me.$refs.PhoneNumber.setClassError();
                                me.$refs.PhoneNumber.setValueMessage(em.ExistValue);
                            }
                            else if(r == "Email"){
                                this.refValue = "Email";
                                me.$refs.Email.setClassError();
                                me.$refs.Email.setValueMessage(em.ExistValue);
                            }
                            
                        }
                        else{
                            me.removeForm(1);
                        }
                    })
                    .catch(res=>{
                        var message = res.response.data.Message.UserMsg;
                        me.$emit("showToastError", message);
                    })
                }
                else{
                    me = this;
                    me.showLoad = true;
                    await Employee.update(d2)
                    .then(res=>{
                        me.showLoad = false;
                        if(res.data.Message.Code == 400){
                            var r = res.data.Message.DevMsg.substring(14);
                            var mess = res.data.Message.UserMsg.substring(21) + " " + em.Exist;
                            me.type = "warning";
                            me.showPopUp = true;
                            me.optionPopUp = [false, false];
                            me.action1 = "";
                            me.action2 = "";
                            me.action3 = "Đồng ý";
                            me.message = mess;

                            if(r == "EmployeeCode"){
                                this.refValue = "EmployeeCode";
                                me.$refs.EmployeeCode.setClassError();
                                me.$refs.EmployeeCode.setValueMessage(em.ExistValue);
                            }
                            else if(r == "IdentityNumber"){
                                this.refValue = "IdentityNumber";
                                me.$refs.IdentityNumber.setClassError();
                                me.$refs.IdentityNumber.setValueMessage(em.ExistValue);
                            }
                            else if(r == "BankAccountNumber"){
                                this.refValue = "BankAccountNumber";
                                me.$refs.BankAccountNumber.setClassError();
                                me.$refs.BankAccountNumber.setValueMessage(em.ExistValue);
                            }
                            else if(r == "PhoneNumber"){
                                this.refValue = "PhoneNumber";
                                me.$refs.PhoneNumber.setClassError();
                                me.$refs.PhoneNumber.setValueMessage(em.ExistValue);
                            }
                            else if(r == "Email"){
                                this.refValue = "Email";
                                me.$refs.Email.setClassError();
                                me.$refs.Email.setValueMessage(em.ExistValue);
                            }
                            
                        }
                        else{
                            me.removeForm(2);
                        }
                    })
                    .catch(res=>{
                        var message = res.response.data.Message.UserMsg;
                        me.$emit("showToastError", message);
                    })
                }
            }
            else{
                //Nếu thêm mới
                if(this.typeForm == 0){
                    me = this;
                    this.showLoad = true;
                    await Employee.addNew(d1)
                    .then(res=>{
                        me.showLoad = false;
                        if(res.data.Message.Code == 400){
                            var r = res.data.Message.DevMsg.substring(14);
                            var mess = res.data.Message.UserMsg.substring(21) + " " + em.Exist;
                            me.type = "warning";
                            me.showPopUp = true;
                            me.optionPopUp = [false, false];
                            me.action1 = "";
                            me.action2 = "";
                            me.action3 = "Đồng ý";
                            me.message = mess;

                            if(r == "EmployeeCode"){
                                this.refValue = "EmployeeCode";
                                me.$refs.EmployeeCode.setClassError();
                                me.$refs.EmployeeCode.setValueMessage(em.ExistValue);
                            }
                            else if(r == "IdentityNumber"){
                                this.refValue = "IdentityNumber";
                                me.$refs.IdentityNumber.setClassError();
                                me.$refs.IdentityNumber.setValueMessage(em.ExistValue);
                            }
                            else if(r == "BankAccountNumber"){
                                this.refValue = "BankAccountNumber";
                                me.$refs.BankAccountNumber.setClassError();
                                me.$refs.BankAccountNumber.setValueMessage(em.ExistValue);
                            }
                            else if(r == "PhoneNumber"){
                                this.refValue = "PhoneNumber";
                                me.$refs.PhoneNumber.setClassError();
                                me.$refs.PhoneNumber.setValueMessage(em.ExistValue);
                            }
                            else if(r == "Email"){
                                this.refValue = "Email";
                                me.$refs.Email.setClassError();
                                me.$refs.Email.setValueMessage(em.ExistValue);
                            }
                            
                        }
                        else{
                            me.resetForm(1);
                            me.$refs.EmployeeCode.focusInput();
                        }
                    })
                    .catch(res=>{
                        var message = res.response.data.Message.UserMsg;
                        me.$emit("showToastError", message);
                    })
                }
                else{
                    me = this;
                    me.showLoad = true;
                    await Employee.update(d2)
                    .then(res=>{
                        me.showLoad = false;
                        if(res.data.Message.Code == 400){
                            var r = res.data.Message.DevMsg.substring(14);
                            var mess = res.data.Message.UserMsg.substring(21) + " " + em.Exist;
                            me.type = "warning";
                            me.showPopUp = true;
                            me.optionPopUp = [false, false];
                            me.action1 = "";
                            me.action2 = "";
                            me.action3 = "Đồng ý";
                            me.message = mess;

                            if(r == "EmployeeCode"){
                                this.refValue = "EmployeeCode";
                                me.$refs.EmployeeCode.setClassError();
                                me.$refs.EmployeeCode.setValueMessage(em.ExistValue);
                            }
                            else if(r == "IdentityNumber"){
                                this.refValue = "IdentityNumber";
                                me.$refs.IdentityNumber.setClassError();
                                me.$refs.IdentityNumber.setValueMessage(em.ExistValue);
                            }
                            else if(r == "BankAccountNumber"){
                                this.refValue = "BankAccountNumber";
                                me.$refs.BankAccountNumber.setClassError();
                                me.$refs.BankAccountNumber.setValueMessage(em.ExistValue);
                            }
                            else if(r == "PhoneNumber"){
                                this.refValue = "PhoneNumber";
                                me.$refs.PhoneNumber.setClassError();
                                me.$refs.PhoneNumber.setValueMessage(em.ExistValue);
                            }
                            else if(r == "Email"){
                                this.refValue = "Email";
                                me.$refs.Email.setClassError();
                                me.$refs.Email.setValueMessage(em.ExistValue);
                            }
                            
                        }
                        else{
                            me.resetForm(2);
                            me.$refs.EmployeeCode.focusInput();
                        }
                    })
                    .catch(res=>{
                        var message = res.response.data.Message.UserMsg;
                        me.$emit("showToastError", message);
                    })
                }
            }
        },

        focusEmployee(){
            this.$refs.EmployeeCode.focusInput();
        },

        //Validate

        /**
         * Tính tuổi của nhân viên
         * @param {mm/dd/yyyy} date ngày sinh
         * @returns tuổi của nhân viên (tính theo ngày tháng năm)
         * Create by: TQHa (15/7/2021)
         */
        checkAge(date) {
            var dob = new Date(date.substring(6), date.substring(3,5), date.substring(0,2));
            
            var monthDiff = Date.now() - dob.getTime();
            var ageDt = new Date(monthDiff);
            var year = ageDt.getUTCFullYear();
            var today = new Date();
            if(dob.getFullYear() > today.getFullYear()){
                return false;
            }
            var age = Math.abs(year - 1970);
            return (age>=18 && age<=60);
        },


        /**
         * Kiểm tra xem cccd/cmnd nhập vào có hợp lệ
         * create by: TQHa (22/7/2021)
         */
        checkDateOfIdentity(dateOfBirth, dateIdentity){
            var dob = new Date(dateOfBirth.substring(6), dateOfBirth.substring(3,5), dateOfBirth.substring(0,2));
            var monthDOB = Date.now() - dob.getTime();
            var ageDt = new Date(monthDOB);
            var yearDOB = ageDt.getUTCFullYear();
            var ageD = Math.abs(yearDOB - 1970);
            var dI = new Date(dateIdentity.substring(6), dateIdentity.substring(3,5), dateIdentity.substring(0,2));
            var monthI = Date.now() - dI.getTime();
            var ageI = new Date(monthI);
            var yearI = ageI.getUTCFullYear(); 
            var ageId = Math.abs(yearI - 1970);
            if((-ageId+ageD) >= 14){
                return true;
            }
            else{
                return false;
            }
        },
    }
}
</script>

<style>
    @import url(../../css/common/autocomplete.css);
    @import url(../../css/content/form.css);
    @import url(../../css/common/datepicker.css);
    .input{
        margin-bottom: 12px;
    }
    .combobox{
        width: 100%;
        border: 1px solid #babec5;
        border-radius: 2px;
        position: relative;
        margin-bottom: 12px;
    }
    .combobox.error{
        border-color: #FF4747 !important;
    }
    .combobox:focus-within{
        border-color: #2ca01c !important;
    }
    .title{
        font-weight: 600;
        font-size: 12px;
        margin-bottom: 4px;
    }
    .title span{
        color: #ff0000;
    }
    .combobox .btn-cbb{
        height: 31px;
        width: 30px;
        position: absolute;
        right: 0;
        top: 0;
        display: flex;
        align-items: center;
        justify-content: center;
    }
    .combobox .btn-cbb:hover{
        background-color: #e0e0e0;
    }
    .combobox .btn-cbb:active{
        background-color: #e0e0e0;
    }
    .combobox .btn-cbb .arrow{
        background-position: -560px -359px;
    }

    

    
</style>