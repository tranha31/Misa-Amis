<template>
    <div class="header">
        <div v-if="!isMini" class="mi mi-24 resize-menu" @click="resizeMenu"></div>
        <div class="company">
            {{company}}
        </div>
        <div class="header-icon">
            <div class="mi mi-14 object-work"></div>
        </div>
        
        <div class="account">
            <div class="mi mi-24 bell"></div>
            <div class="mi mi-32 account-avatar"></div>
            <div class="account-name">{{account}}</div>
            <div class="mi mi-14 account-option"></div>
        </div>
        
    </div>
</template>

<script>
export default {
    name: "Header",
    props:{
        company: String,
        account: String,
        isMini: Boolean,
    },
    methods:{
        /**
         * Thu nhỏ menu
         * create by: TQHA (17/8/2021)
         */
        resizeMenu(){
            this.$emit("resizeMenu");
        }
    }
}
</script>

<style>

@import url(../../css/header/header.css);

</style>