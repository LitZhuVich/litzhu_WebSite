import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import { App } from "vue";

const routes: RouteRecordRaw[] = [
	{
		path: "/",
		name: "Content",
		components: {
			main: () => import("../views/Content.vue"),
		},
		meta: {
			name: "首页",
		},
	},
	{
		path: "/article/:articleId",
		name: "ArticleDesc",
		components: {
			main: () => import("../views/ArticleDesc.vue"),
		},
		meta: {
			name: "博客详情",
		},
	},
	{
		path: "/tags",
		name: "Tags",
		components: {
			main: () => import("../views/Article/Tags.vue"),
		},
		meta: {
			name: "标签页",
		},
	},
	{
		path: "/tags/:tagName",
		name: "TagArticles",
		components: {
			main: () => import("../views/TagArticles.vue"),
		},
		meta: {
			name: "标签文章",
		},
	},
	{
		path: "/message",
		name: "Message",
		components: {
			main: () => import("../views/Other/Message.vue"),
		},
		meta: {
			name: "留言板",
		},
	},
	{
		path: "/chat",
		name: "Chat",
		components: {
			main: () => import("../views/Other/Chat.vue"),
		},
		meta: {
			name: "聊天室",
		},
	},
	{
		path: "/animation",
		name: "Animation",
		components: {
			main: () => import("../views/Other/Animation.vue"),
		},
		meta: {
			name: "追番列表",
		},
	},
	{
		path: "/login",
		name: "Login",
		components: {
			main: () => import("../views/Auth/Login.vue"),
		},
		meta: {
			name: "登录",
		},
	},
	{
		path: "/register",
		name: "Register",
		components: {
			main: () => import("../views/Auth/Register.vue"),
		},
		meta: {
			name: "注册",
		},
	},
];

const router = createRouter({
	// 使用历史路由
	history: createWebHistory(),
	routes, // 路由配置
});

// 封装初始化路由
export const initRouter = (app: App<Element>) => {
	app.use(router);
};
