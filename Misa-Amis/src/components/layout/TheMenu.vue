<template>
    <div :class="[isMini ? 'menu-bar-mini' : 'menu']">
        <div v-if="!isMini" class="logo-amis">
            <div class="menu-container-option"></div>
            <div class="logo"></div>
        </div>
        <div v-else class="mini-menu">
            <div class="resize-menu" @click="resizeMenu"></div>
        </div>
        <div class="menu-item-list">
            <router-link to="/">
                <TheMenuItem
                typeItem="menu-item-overview"
                content="Tổng quan"
                :isMini="isMini"
                :isChoose="choose[0]"
                v-on:chooseItem="chooseItem(0)"
                />
            </router-link>
           <router-link to="/employee">
                <TheMenuItem
                typeItem="menu-item-cash"
                content="Tiền mặt"
                :isMini="isMini"
                :isChoose="choose[1]"
                v-on:chooseItem="chooseItem(1)"
                />
            </router-link>
            <router-link to="/">
                <TheMenuItem
                typeItem="menu-item-send"
                content="Tiền gửi"
                :isMini="isMini"
                :isChoose="choose[2]"
                v-on:chooseItem="chooseItem(2)"
                />
            </router-link>
            <router-link to="/">
                <TheMenuItem
                typeItem="menu-item-buy"
                content="Mua hàng"
                :isMini="isMini"
                :isChoose="choose[3]"
                v-on:chooseItem="chooseItem(3)"
                />
            </router-link>
            <router-link to="/">
                <TheMenuItem
                typeItem="menu-item-sell"
                content="Bán hàng"
                :isMini="isMini"
                :isChoose="choose[4]"
                v-on:chooseItem="chooseItem(4)"
                />
            </router-link>
            <router-link to="/">
                <TheMenuItem
                typeItem="menu-item-bill"
                content="Quản lý hóa đơn"
                :isMini="isMini"
                :isChoose="choose[5]"
                v-on:chooseItem="chooseItem(5)"
                />
            </router-link>
            <router-link to="/">
                <TheMenuItem
                typeItem="menu-item-depot"
                content="Kho"
                :isMini="isMini"
                :isChoose="choose[6]"
                v-on:chooseItem="chooseItem(6)"
                />
            </router-link>
            <router-link to="/">
                <TheMenuItem
                typeItem="menu-item-tool"
                content="Công cụ dụng cụ"
                :isMini="isMini"
                :isChoose="choose[7]"
                v-on:chooseItem="chooseItem(7)"
                />
            </router-link>
            <router-link to="/">
                <TheMenuItem
                typeItem="menu-item-asset"
                content="Tài sản cố định"
                :isMini="isMini"
                :isChoose="choose[8]"
                v-on:chooseItem="chooseItem(8)"
                />
            </router-link>
            <router-link to="/">
                <TheMenuItem
                typeItem="menu-item-tax"
                content="Thuế"
                :isMini="isMini"
                :isChoose="choose[9]"
                v-on:chooseItem="chooseItem(9)"
                />
            </router-link>
            <router-link to="/">
                <TheMenuItem
                typeItem="menu-item-price"
                content="Giá thành"
                :isMini="isMini"
                :isChoose="choose[10]"
                v-on:chooseItem="chooseItem(10)"
                />
            </router-link>
            <router-link to="/">
                <TheMenuItem
                typeItem="menu-item-total"
                content="Tổng hợp"
                :isMini="isMini"
                :isChoose="choose[11]"
                v-on:chooseItem="chooseItem(11)"
                />
            </router-link>
            <router-link to="/">
                <TheMenuItem
                typeItem="menu-item-budget"
                content="Ngân sách"
                :isMini="isMini"
                :isChoose="choose[12]"
                v-on:chooseItem="chooseItem(12)"
                />
            </router-link>
            <router-link to="/">
                <TheMenuItem
                typeItem="menu-item-report"
                content="Báo cáo"
                :isMini="isMini"
                :isChoose="choose[13]"
                v-on:chooseItem="chooseItem(13)"
                />
            </router-link>
            <router-link to="/">
                <TheMenuItem
                typeItem="menu-item-finance"
                content="Phân tích tài chính"
                :isMini="isMini"
                :isChoose="choose[14]"
                v-on:chooseItem="chooseItem(14)"
                />
            </router-link>
        </div>
    </div>
</template>

<script>
import TheMenuItem from './TheMenuItem.vue'
export default {
    name: "Menu",
    components: { TheMenuItem },
    props:{
        isMini: Boolean
    },
    data(){
        return{
            choose: [false, true, false, false, false, false, false, false, false, false, false, false, false, false, false,]
        }
    },
    methods:{
        /**
         * Lựa chọn item
         * @param {Number} index thứ tự của item 
         * create by: TQHA (17/8/2021)
         */
        chooseItem(index){
            var tmp = [];
            for(var i=0; i<15; i++){
                (i == index) ? tmp.push(true) : tmp.push(false);
            }
            this.choose = tmp;
        },
        /**
         * Truyền sự kiện phóng to menu cho component cha
         * create by: TQHa (17/8/2021)
         */
        resizeMenu(){
            this.$emit("resizeMenu");
        }
    }
}
</script>

<style>
    .menu-bar-mini{
        width: 52px;
        background-color: #393A3D;
        display: flex;
        flex-direction: column;
    }
    .menu-bar-mini .mini-menu{
        height: 48px;
        display: flex;
        align-items: center;
        justify-content: center;
    }
    .menu-bar-mini .mini-menu .resize-menu{
        background: url(../../assets/img/Sprites.64af8f61.svg) no-repeat -316px -37px;
        width: 16px;
        height: 14px;
        cursor: pointer;
    }
    .menu{
        width: 178px;
        height: 100%;
        background-color: #393A3D;
        display: flex;
        flex-direction: column;
    }
    .menu .logo-amis{
        padding: 0px 24px; 
        height: 48px;
        display: flex;
        align-items: center;
    }
    .menu .logo-amis .menu-container-option{
        width: 24px;
        height: 24px;
        margin-right: 10px;
        background: url(../../assets/img/Sprites.64af8f61.svg) no-repeat -424px -86px;
        cursor: pointer;
    }
    .menu .logo-amis .logo{
        flex: 1;
        height: 32px;
        background-image: url(../../assets/img/Logo_Module_TiengViet_White.66947422.svg);
        background-repeat: no-repeat;
        background-size: contain;
    }
    .menu-item-list{
        height: calc(100vh - 48px);
        padding-top: 14px;
        display: flex;
        flex-direction: column;
    }
</style>