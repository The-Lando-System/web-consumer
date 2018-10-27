import Vue from 'vue';
import VueResource from 'vue-resource';
import App from './App.vue';
import Broadcaster from './services/Broadcaster.js';
import RequestService from './services/RequestService.js';

// Define global services on the Vue application
Object.defineProperty(Vue.prototype, '$broadcaster', { value: Broadcaster });
Object.defineProperty(Vue.prototype, '$requestSvc', { value: RequestService });

Vue.config.productionTip = false;
Vue.use(VueResource);

new Vue({
  render: h => h(App)
}).$mount('#app')
