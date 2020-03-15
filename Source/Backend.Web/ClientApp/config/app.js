import Vue from "vue";
import EventBus from "root/event-bus";
import axios from "axios";
import store from "./store";
import router from "./router";
import { sync } from "vuex-router-sync";
import App from "components/app/index";
// import "./registerServiceWorker";
import moment from "moment-timezone";
moment.locale("pt-br");
moment.tz.setDefault("America/Sao_Paulo");

Vue.prototype.$bus = EventBus;
Vue.prototype.$http = axios;
Vue.prototype.$moment = moment;

Vue.config.productionTip = false;
Vue.config.devtools = false;

sync(store, router);

const app = new Vue({
  store,
  router,
  ...App
});

export { app, router, store };
