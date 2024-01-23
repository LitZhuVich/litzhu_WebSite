import { createRouter, createWebHistory } from "vue-router";

import { App } from "vue";

const routes = [
	{
		path: "/",
		name: "FrontView",
		components: {
			main: () => import("../views/FrontView.vue"),
		},
	},
];

// 创建路由
const router = createRouter({
	// 使用历史路由
	history: createWebHistory(),
	routes, // 路由配置
});

// 封装初始化路由
export const initRouter = (app: App<Element>) => {
	app.use(router);
};
