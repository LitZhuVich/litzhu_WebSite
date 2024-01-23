<template>
	<el-menu
		:default-active="activeIndex"
		class="el-menu"
		mode="horizontal"
		router
		@select="handleSelect">
		<el-menu-item index="/">
			<span class="text-8">LitZhu</span>
		</el-menu-item>
		<el-menu-item index="/">首页</el-menu-item>
		<el-sub-menu index="article">
			<template #title>文章</template>
			<el-menu-item index="/tag">标签</el-menu-item>
			<el-menu-item index="/archives">归档</el-menu-item>
		</el-sub-menu>
		<el-menu-item index="/BB">胡说空间</el-menu-item>
		<el-sub-menu index="other">
			<template #title>其他</template>
			<el-menu-item index="/chat">聊天室</el-menu-item>
			<el-menu-item index="/message">留言版</el-menu-item>
			<el-menu-item index="/bangumis">追番列表</el-menu-item>
		</el-sub-menu>
		<el-menu-item index="/about">关于</el-menu-item>
		<el-menu-item>
			<el-icon><Search /></el-icon>
		</el-menu-item>
		<el-menu-item index="/login">
			<el-icon><User /></el-icon>
		</el-menu-item>
		<el-menu-item :class="{ dark: isDark }">
			<el-switch
				v-model="isDark"
				@change="toggleDark"
				:active-action-icon="Moon"
				:inactive-action-icon="Sunny" />
		</el-menu-item>
	</el-menu>
</template>

<script setup lang="ts">
	import { ref } from "vue";
	import { Sunny, Moon } from "@element-plus/icons-vue";
	// 切换主题
	import { useDark, useToggle } from "@vueuse/core";
	const isDark = useDark();
	const toggleDark = useToggle(isDark);
	// 目录默认打开
	const activeIndex = ref("1");
	const handleSelect = (key: string, keyPath: string[]) => {
		console.log(key, keyPath);
	};
</script>

<style lang="scss" scoped>
	.el-menu {
		justify-content: center;
	}
</style>
