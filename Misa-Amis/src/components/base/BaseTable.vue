<template>
    <div class="grid-content">
        <table class="grid-table">
            <thead>
                <tr>
                    <th class="th-employee white-cell"></th>
                    <th class="th-viewer th-checkbox th-employee">
                        <label class="checkbox" @click="chooseAll($event)"></label>
                    </th>
                    <th v-for="(th, index) in listTh" v-bind:key="index" class="th-viewer" :class="th.class">
                        {{th.content}}
                    </th>
                    <th class="white-cell-right"></th>
                    <th class="grey-cell"></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(d, index) in datas" v-bind:key="index" @click="chooseTr(index)" @dblclick="editEmployee(index)" :id="index">
                    <td class="white-cell"></td>
                    <td class="td-view td-checkbox">
                        <label class="checkbox" @click="checkBox($event, index)"></label>
                    </td>
                    <td class="td-view employee-code">{{ d.EmployeeCode }}</td>
                    <td class="td-view employee-name">{{ d.EmployeeName }}</td>
                    <td class="td-view employee-gender">{{ checkNull(d.GenderName) }}</td>
                    <td class="td-view employee-dob">{{ formatDate(d.DateOfBirth) }}</td>
                    <td class="td-view employee-identity">{{ checkNull(d.IdentityNumber) }}</td>
                    <td class="td-view employee-position">{{ checkNull(d.EmployeePosition) }}</td>
                    <td class="td-view employee-department">{{ d.DepartmentName }}</td>
                    <td class="td-view employee-account">{{ checkNull(d.BankAccountNumber) }}</td>
                    <td class="td-view employee-bank-name">{{ checkNull(d.BankName) }}</td>
                    <td class="td-view employee-bank-branch">{{ checkNull(d.BankBranchName)}}</td>
                    <td class="td-view employee-option" :id="'function-'+index">
                        <div class="function">
                            <button class="edit" @click="editEmployee(index)">Sửa</button>
                            <button class="mi mi-16 option show" @click="showFunction(index)" ></button>
                        </div>
                        <div class="list-option hidden-function" :id="'listOption-'+index">
                            <div class="option" @click="copyEmployee(index)">Nhân bản</div>
                            <div class="option" @click="deleteEmployee(index)">Xóa</div>
                        </div>

                    </td>
                    <td class="white-cell-right"></td>
                    <td class="grey-cell"></td>
                    
                </tr>
                
                
                
            </tbody>
        </table>
        
    </div>
</template>

<script>

export default {
    name: "Table",
    components: { },
    props:{
        listTh : Array,
        datas : Array, 
    },
    data() {
        return {
            indexTr : -1,
        }
    },
    methods:{
        /**
         * Kiểm tra nội dung có rỗng
         * create by: TQHA (19/8/2021)
         */
        checkNull(value){
            return (value != null && value!= undefined && value!="") ? value : "";
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
         * Hiển thị nội dung các lựa chọn
         * create by: TQHA (19/8/2021)
         */
        showFunction(index){
            
            if(index == this.indexTr){
                document.getElementById(`function-${index}`).style.zIndex = "2";
                document.getElementById(`listOption-${index}`).classList.add("hidden-function");
                this.indexTr = -1;
            }
            else{
                var divOption = document.querySelectorAll(".list-option");
                divOption.forEach(element => {
                    element.classList.add("hidden-function");
                });
                
                var tdOption = document.querySelectorAll(".employee-option");
                tdOption.forEach(element => {
                    element.style.zIndex = "2";
                });

                document.getElementById(`function-${index}`).style.zIndex = "5";
                document.getElementById(`listOption-${index}`).classList.remove("hidden-function");

                this.indexTr = index;
                
            }
            
        },
        /**
         * Click vào hàng 
         * create by: TQHA (19/8/2021)
         */
        chooseTr(index){
            var tr = document.querySelectorAll("tr");
            tr.forEach(element => {
                element.classList.remove("choosed");
            });
            document.getElementById(index).classList.add("choosed");
        },

        /**
         * Chọn nhân viên để xóa
         * create by: TQHA (19/8/2021)
         */
        checkBox(event, index){
            if(event.target.classList.contains("checkbox-active")){

                event.target.classList.remove("checkbox-active");
            }
            else{
                event.target.classList.add("checkbox-active");
            }
            this.chooseTr(index);
            this.$emit("addEmployeeDelete", index);
        },

        /**
         * Chọn tất cả để xóa
         * create by: TQHA (19/8/2021)
         */
        chooseAll(event){
            var label = document.querySelectorAll(".checkbox");
            if(event.target.classList.contains("checkbox-active")){
                label.forEach(element=>{
                    element.classList.remove("checkbox-active");
                });
                this.$emit("chooseAllEmployee", 0);    
            }
            else{
                label.forEach(element=>{
                    element.classList.add("checkbox-active");
                });
                this.$emit("chooseAllEmployee", 1);
            }
            
        },

        unChooseAll(){
            var label = document.querySelectorAll(".checkbox");
            label.forEach(element=>{
                element.classList.remove("checkbox-active");
            });

            var divOption = document.querySelectorAll(".list-option");
            divOption.forEach(element => {
                element.classList.add("hidden-function");
            });
            
            var tdOption = document.querySelectorAll(".employee-option");
            tdOption.forEach(element => {
                element.style.zIndex = "2";
            });

        },
        /**
         * Xóa nhân viên
         * create by: TQHA (19/8/2021)
         */
        deleteEmployee(index){
            var divOption = document.querySelectorAll(".list-option");
            divOption.forEach(element => {
                element.classList.add("hidden-function");
            });
            this.$emit("deleteEmployee", index);
        },
        /**
         * Hiện form chỉnh sửa thông tin nhân viên
         * create by: TQHA (20/8/2021)
         */
        editEmployee(index){
            var divOption = document.querySelectorAll(".list-option");
            divOption.forEach(element => {
                element.classList.add("hidden-function");
            });
            this.$emit("editEmployee", index)
        },
        /**
         * Hiện form nhân bản thông tin nhân viên
         * create by: TQHA (20/8/2021)
         */
        copyEmployee(index){
            var divOption = document.querySelectorAll(".list-option");
            divOption.forEach(element => {
                element.classList.add("hidden-function");
            });
            this.$emit("copyEmployee", index);
        }
    },
}
</script>

<style>
    @import url(../../css/common/table.css);
    .list-option{
        width: 120px;
        
        border: 1px solid #babec5;
        background-color: #ffffff;
        position: absolute;
        top: 35px;
        right: 20px;
        z-index: 999;
        

    }
    .list-option .option{
        padding: 5px 10px;
        cursor: pointer;
    }
    .list-option .option:hover{
        background-color: #e8e9ec;
        color: #15c12a;
    }
    .hidden-outside{
        position: fixed;
        height: 100%;
        top: 48px;
        right: 10px;
        width: 16px;
        background-color: #f8f8f8;
        z-index: 80;
    }
    .hidden-white{
        position: fixed;
        width: 30px;
        right: 0px;
        height: calc(100% - 200px);
        top: 190px;
        right: 26px;
        z-index: 85;
        background-color: #ffffff;
    }
</style>