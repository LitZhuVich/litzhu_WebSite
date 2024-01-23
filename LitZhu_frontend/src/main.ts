import { createApp } from "vue";
import "./style.css";
import "virtual:uno.css";
import App from "./App.vue";
// 暗色模式
import "element-plus/theme-chalk/dark/css-vars.css";
// 引入 路由文件
import { initRouter } from "./router/router";
// 引入pinia的createPinia方法
import { createPinia } from "pinia";
const pinia = createPinia();
const app = createApp(App);
// 初始化路由
initRouter(app);
// 注册所有图标 TODO:修改成自动导入https://element-plus.org/zh-CN/component/icon.html
import * as ElementPlusIconsVue from "@element-plus/icons-vue";
for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
	app.component(key, component);
}

app.use(pinia);
app.mount("#app");
