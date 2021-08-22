import Vue from 'vue'
import App from './App.vue'
import VueRouter from 'vue-router'
import Employee from './views/Employee'
import VTooltip from 'v-tooltip'
import Autocomplete from 'v-autocomplete'

import 'v-autocomplete/dist/v-autocomplete.css'

Vue.use(VueRouter);
Vue.use(VTooltip);
Vue.use(Autocomplete)
const routes = [
  {path: '/employee', name: 'employee', component: Employee},
]


Vue.config.productionTip = false
const router = new VueRouter({
  mode: 'history',
  base : process.env.BASE_URL,
  routes
})

// Check click outside components
Vue.directive("click-outside", {
  bind: function(element, binding, vnode) {
    element.clickOutsideEvent = function(event) {
      //  check that click was outside the el and his children
      if (!(element === event.target || element.contains(event.target))) {
        // and if it did, call method provided in attribute value
        vnode.context[binding.expression](event);
        // binding.value(); run the arg
      }
    };
    document.body.addEventListener("click", element.clickOutsideEvent);
  },
  unbind: function(element) {
    document.body.removeEventListener("click", element.clickOutsideEvent);
  },
});


new Vue({
  router,
  render: h => h(App),
}).$mount('#app')
