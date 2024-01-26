import { createRouter, createWebHistory } from "vue-router";

import { App } from "vue";

const routes = [
	{
		path: "/",
		name: "Content",
		components: {
			main: () => import("../views/Front/Content.vue"),
		},
		meta: {
			name: "首页",
		},
	},
	{
		path: "/article/:articleTitle",
		name: "ArticleDesc",
		components: {
			main: () => import("../views/Front/ArticleDesc.vue"),
		},
		meta: {
			name: "博客详情",
		},
	},
	{
		path: "/tags",
		name: "Tags",
		components: {
			main: () => import("../views/Front/Tags.vue"),
		},
		meta: {
			name: "标签页",
		},
	},
	{
		path: "/tags/:tagName",
		name: "TagArticles",
		components: {
			main: () => import("../views/Front/TagArticles.vue"),
		},
		meta: {
			name: "标签文章",
		},
	},
	{
		path: "/message",
		name: "Message",
		components: {
			main: () => import("../views/Front/Message.vue"),
		},
		meta: {
			name: "留言板",
		},
	},
	{
		path: "/chat",
		name: "Chat",
		components: {
			main: () => import("../views/Front/Chat.vue"),
		},
		meta: {
			name: "聊天室",
		},
	},
	{
		path: "/animation",
		name: "Animation",
		components: {
			main: () => import("../views/Front/Animation.vue"),
		},
		meta: {
			name: "追番列表",
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
