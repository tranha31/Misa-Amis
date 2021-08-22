<template>
    <div class="router-view">
        <div class="router-content">
            <TheContentEmployee
            :isMini="isMini"
            />
        </div>
        
    </div>
</template>

<script>
import TheContentEmployee from '../components/layout/TheContentEmployee.vue'
export default {
    name: "Employee",
    components: { TheContentEmployee },
    props:{
       isMini: Boolean,
    },
    methods:{
        
    }
}
</script>

<style>
    .router-view{
        flex: 1;
        background-color: #f8f8f8;
    }
    .router-view .router-content{
        padding-left: 20px;
        height: calc(100vh - 48px);
    }
</style>