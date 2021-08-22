<template>
  <div id="app">
    <TheMenu :isMini="mini" v-on:resizeMenu="resizeMenu"/>
    <div class="content">
      <TheHeader
        :company="company"
        :account="account"
        :isMini="mini"
        v-on:resizeMenu="resizeMenu"
      />

      <router-view :isMini="mini"></router-view>
    </div>
    
  </div>
</template>

<script>
import TheHeader from './components/layout/TheHeader.vue';
import TheMenu from './components/layout/TheMenu.vue'

export default {
  name: 'App',
  components: {
    TheMenu,
    TheHeader
   
  },
  data(){
    return{
      mini: false,
      account: "Trần Quang Hà",
      company: "CÔNG TY TNHH SẢN XUẤT - THƯƠNG MẠI - DỊCH VỤ QUI PHÚC"
    }
  },
  methods:{
    /**
     * Thay đổi kích thước menu
     * create by: TQHA (17/8/2021)
     */
    resizeMenu(){
      this.mini = !this.mini;
    }
  }
}
</script>

<style>

@import url(css/common/common.css);

  #app{
    display: flex;
    height: 100vh;
    width: 100vw;
  }
 .content{
    position: relative !important;
    display: flex;
    flex-direction: column;
    flex: 1;
  }
</style>
