<template>
<div class="container" style="position:relative">
    <div class="dropdown-container">
        <input type="text" class="dropdown-content" readonly :value="value" :id="id" v-on:keyup.38="selectItem" v-on:keyup.40="selectItemDown" v-on:keyup.13="enterChoose" :tabindex="tabindex">
        <div class="button" @click="showOption" v-click-outside="hiddenOption">
            <div class="mi mi-16 dropdown-button" :class="[isShow? 'choose' : '']"></div>
        </div>
        
    </div>
    <div :class="[isShow? 'option-container' : 'hidden-option']">
        <div v-for="(o, index) in listOption" v-bind:key="index" :title="o">
            <input type="radio" :value="o" :id="'option'+index" name="amount" class="option-radio" style="display: none" :checked="(o == value) ? 'checked' : ''" @click="chooseOption(o, index)">
            <label :for="'option'+index"  class="option">{{o}}</label>
        </div>
    </div>
    <div :tabindex="tabindex+1" @focus="hiddenShowOption"></div>
</div>

</template>

<script>
export default {
    name: "DropDown",
    props:{
        value : String,
        listOption: Array,
        id: String,
        tabindex: Number,
    },
    data(){
        return{
            isShow: false,
            indexOption : 0,
        }
    },
    computed:{
        
    },
    methods:{
        /**
         * Hiển thị nội dung option
         * create by: TQHa (19/8/2021)
         */
        showOption(){
            this.isShow = !this.isShow;
            if(this.isShow){
                document.getElementById(this.id).focus();
            }
        },
        /**
         * Click ra ngoài thì đóng option
         * create by: TQHa (19/8/2021)
         */
        hiddenOption(event){
            var div = document.getElementById(this.id);
            (event.target ==  div)? this.isShow = true : this.isShow = false;
        },
        /**
         * Chọn option
         * create by: TQHa (19/8/2021)
         */
        chooseOption(value, index){
            this.$emit("chooseOption", value);
            this.indexOption = index;
            this.$emit("confirmOption", index);
        },
        /**
         * Chọn option bằng cách nhấn phím đi lên
         * create by: TQHa (19/8/2021)
         */
        selectItem(){
            var option = document.querySelectorAll(".option-radio");
            var index;
            for(var i=0; i<option.length; i++){
                if(option[i].checked){
                    var idO = option[i].id;
                    idO = idO.substring(6);
                    index = idO;
                    break;
                }
            }
            if(index != 0){
                var value = option[index-1].value;
                this.$emit("chooseOption", value);
                this.indexOption = index-1;
            }
        },
        /**
         * Chọn option bằng cách nhấn phím đi xuống
         * create by: TQHA (19/8/2021)
         */
        selectItemDown(){
            var option = document.querySelectorAll(".option-radio");
            var index;
            for(var i=0; i<option.length; i++){
                if(option[i].checked){
                    var idO = option[i].id;
                    idO = idO.substring(6);
                    index = idO;
                    break;
                }
            }
            index = Number(index);
            if(index != option.length-1){
                var value = option[index+1].value;
                this.$emit("chooseOption", value);
                this.indexOption = index+1;
            }
        },
        /**
         * Nhấn enter để lọc
         * create by: TQHA (19/8/2021)
         */
        enterChoose(){
            this.isShow = false;
            document.getElementById(this.id).blur();
            this.$emit("confirmOption", this.indexOption);
            
        },
        /**
         * Đóng option khi nhấn tab
         * create by: TQHA (19/8/2021)
         */
        hiddenShowOption(){
            this.isShow = false;
        }
    },
}
</script>

<style>
    .dropdown-container{
        width: 200px;
        height: 32px;
        
        display: flex;
    }
    .dropdown-container .dropdown-content{
        border: 1px solid #babec5;
        outline: none;
        padding-left: 10px;
        flex: 1;
        cursor: pointer;
    }
    .dropdown-container .dropdown-content:focus{
        border-color: #2ca01c !important;
    }
    .dropdown-container .button{
        width: 30px;
        height: 31px;
        display: flex;
        align-items: center;
        justify-content: center;
        cursor: pointer;
        position: absolute;
        right: 0;
        border: none;
    }
    .dropdown-container .button:hover{
        background-color: #e0e0e0;
    }
    .dropdown-container .button .dropdown-button{
        background-position: -560px -359px;
    }

    .option-container{
        width: 200px;
        height: auto;
        position: absolute;
        bottom: 34px;
        background-color: #ffffff;
        display: flex;
        flex-direction: column;
        border: 1px solid #babec5;
    }

    .option-container .option{
        height: 32px;
        width: 100%;
        padding-left: 10px;
        display: flex;
        align-items: center;
        background-color: #ffffff;
        cursor: pointer;
    }
    .option-container .option:hover{
        background-color: #ebedf0;
        color: #6cba63;
    }
    .option-radio:checked + label{
        background-color: #2ca01c !important;
        color: #ffffff !important;
    }
    .hidden-option{
        display: none;
    }
    .choose{
        transform: rotate(180deg);
    }
</style>