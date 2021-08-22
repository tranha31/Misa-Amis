<template>
    <div class="container-input" :class="clas" v-tooltip.bottom="message">
        <div class="title">{{title}}&nbsp;<span class="required">{{required}}</span></div>
        <input 
        type="text" 
        :class="['input-content', error? 'error' : '']" 
        :maxlength="maxL" 
        v-model="values" 
        ref="inputValue"
        @blur="checkValidate"
        @input="inputValue"
        @keypress="isNumberInput($event)"
        >
    </div>
</template>

<script>

import EmployeeForm from '../../js/entity/EmployeeForm.js';

export default {
    name: "Input",
    props:{
        required : String,
        title: String,
        clas: String,
        maxL: String,
        value: String,
        notEmpty: Boolean,
        isNumber: Boolean,
        phoneNumber: Boolean,
        telephoneNumber: Boolean,
        identity: Boolean,
        email: Boolean,
        employeeCode: Boolean,
        employeeName: Boolean,
    },
    data(){
        return{
            values: this.value,
            message : '',
            error: false,
        }
    },
    watch:{
        value: function () {
            this.values = this.value;
        },
    },
    methods:{
        /**
         * Focus vào ô nhập liệu
         * create by: TQHa (20/8/2021)
         */
        focusInput(){
            this.$refs.inputValue.focus();
        },
        /**
         * Kiểm tra dữ liệu khi blur
         * create by: TQHA (21/8/2021)
         */
        checkValidate(){
            var error = new EmployeeForm();
            if(this.notEmpty){
                if(this.values == "" || this.values == null){
                    if(this.employeeCode){
                        this.setValueMessage(error.EmployeeCodeEmpty);
                    }
                    else if(this.employeeName){
                        this.setValueMessage(error.EmployeeNameEmpty);
                    }
                    this.error = true;
                }
                else{
                    if(this.employeeCode){
                        if(!this.checkEmployeeCode(this.values)){
                            this.setValueMessage(error.InvalidCode);
                            this.error = true;
                        }
                        else{
                            this.setValueMessage("");
                        }
                    }
                    if(this.employeeName){
                        if(!this.checkFullName(this.values)){
                            this.setValueMessage(error.InvalidName);
                            this.error = true;
                        }
                        else{
                            this.setValueMessage("");
                        }
                    }
                    
                    
                }
            }
            else{
                if(this.values != "" && this.values != null){
                    if(this.phoneNumber){
                        if(!this.checkPhoneNumber(this.values)){
                            this.setValueMessage(error.InvalidPhone);
                            this.error = true;
                        }
                        else{
                            this.setValueMessage("");
                        }
                    }

                    if(this.telephoneNumber){
                        if(!this.checkTelePhone(this.values)){
                            this.setValueMessage(error.InvalidTelePhone);
                            this.error = true;
                        }
                        else{
                            this.setValueMessage("");
                        }
                    }
                    if(this.identity){
                        if(!this.checkIndentityCode(this.values)){
                            this.setValueMessage(error.InvalidIdentity);
                            this.error = true;
                        }
                        else{
                            this.setValueMessage("");
                        }
                    }
                    if(this.email){
                        if(!this.checkEmail(this.values)){
                            this.setValueMessage(error.InvalidEmail);
                            this.error = true;
                        }
                        else{
                            this.setValueMessage("");
                        }
                    }
                }
            }

        },
        /**
         * Nhập dữ liệu
         * create by: TQHA (21/8/2021)
         */
        inputValue(){
            this.error = false;
            this.setValueMessage("");
        },
        /**
         * Đặt nội dung cho tooltip
         * create by: TQHA (21/8/2021)
         */
        setValueMessage(content){
            this.message = content;
        },
        /**
         * Thêm class error cho input
         * create by: TQHa (21/8/2021)
         */
        setClassError(){
            this.error = true;
        },
        /**
         * Chỉ cho nhập số
         * create by: TQHa (21/8/2021)
         */
        isNumberInput(evt) {
            if (this.isNumber) {
                evt = evt ? evt : window.event;
                var charCode = evt.which ? evt.which : evt.keyCode;
               
                if (
                    charCode > 31 &&
                    (charCode < 48 || charCode > 57) &&
                    charCode !== 46
                ) {
                    evt.preventDefault();
                } 
                else {
                    return true;
                }
                
                
            }
        },

        /**
         * Kiểm tra số điện thoại có hợp lệ
         * @param {String} phone
         * @returns đúng hoặc sai
         * create by: TQHa (7/7/2021)
         */
        checkPhoneNumber(phone) {
            var numbers = /((09|03|07|08|05|84)+([0-9]{8})\b)/g;
            var numb = /((84)+([0-9]{9})\b)/g;
            var n = /([+]+(84)+([0-9]{9})\b)/g;
            var num = /((02)+([0-9]{9})\b)/g;
            if (
                phone.match(numbers) ||
                phone.match(num) ||
                phone.match(numb) ||
                phone.match(n)
            ) {
                return true;
            } 
            else {
                return false;
            }
        },
        /**
         * Kiểm tra số điện thoại bàn
         * create by: TQHA (21/8/2021)
         */
        checkTelePhone(phone){
            var number = /(\([0-9]{3}\)\s[0-9]{3}-[0-9]{4}\b)/g;
            if(phone.match(number)){
                return true;
            }
            else{
                return false;
            }
        },

        /**
         * Kiểm tra email nhập vào có hợp lệ
         * @param {String} email
         * @returns đúng hoặc sai
         * Create by: TQHa (15/7/2021)
         */
        checkEmail(email) {
            const re =
                /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            return re.test(String(email).toLowerCase());
        },

        /**
         * Kiểm tra tên nhập có hợp lệ
         * @param {String} name
         * @returns hợp lệ hoặc không
         * create by : TQHa (15/7/2021)
         */
        checkFullName(name) {
            var result = true;
            const regex = /^[a-zA-Z_ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơớờợợƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹý\s\\.]+$/;
            if(regex.test(name)){
                var indexDot = 0;
                var indexSpace = 0;
                for(var i=0; i<name.length; i++){
                if(name[i] == '.'){
                    if(i == indexDot+1){
                    result = false;
                    break;
                    }
                    else{
                    indexDot = i;
                    }
                }
                }
                for(i=0; i<name.length; i++){
                if(name[i] == ' '){
                    if(i == indexSpace+1){
                    result = false;
                    break;
                    }
                    else{
                    indexSpace = i;
                    }
                }
                }
            }
            else{
                result = false;
            }
            
            return result;
        },

        /**
         * Kiểm tra mã nhân viên có hợp lệ
         * create by: TQHa (10/8/2021)
         */
        checkEmployeeCode(code){
            const regex = /^NV-[0-9]+$/;
            return regex.test(code);
        },
        
        /**
         * Kiểm tra độ dài số CMND/CCCD
         * @param {Number} code số CMND/CCCD
         * @returns đúng hoặc sai
         * Create by: TQHa (15/7/2021)
         */
        checkIndentityCode(code) {
            if (code.length == 9 || code.length == 12) {
                return true;
            } else {
                return false;
            }
        },
    },
}
</script>

<style>
    @import url(../../css/common/input.css);
</style>