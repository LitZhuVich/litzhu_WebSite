<template>
	<el-carousel :interval="interval" height="auto" trigger="click" class="banner">
		<el-carousel-item v-for="item in 6" :key="item" class="item">
			<img src="/image/user_tou.jpg" class="w-full h-full object-cover" />
			<el-card class="flex flex-col justify-between">
				<template #header>
					<span class="text-8 font-bold">标题</span>
					<el-tag type="warning" v-for="item in 3" class="ml-2">后端 {{ item }}</el-tag>
				</template>
				<el-text type="info">asiodjsadiosajasiod</el-text>
				<template #footer>
					<div class="flex flex-justify-start content-center">
						<el-avatar :size="30" src="/image/user_tou.jpg" class="mr-2" />
						<el-text><span class="mr-2">用户名</span></el-text>
						<el-text type="info"><span class="mr-2">发布于：时间</span></el-text>
					</div>
				</template>
			</el-card>
		</el-carousel-item>
	</el-carousel>
</template>

<script setup lang="ts">
	import { ref } from "vue";
	const interval = ref(5000); // 轮播时间
</script>

<style lang="scss" scoped>
	@media (max-width: 768px) {
		.item {
			height: 600px !important;
			grid-template-columns: 1fr !important;
			grid-template-rows: 1fr 1fr !important;
		}
		img {
			height: 300px !important;
		}
	}
	.banner {
		box-shadow: var(--el-box-shadow-light);
		border-radius: 1rem;
		.item {
			height: 22rem;
			display: grid;
			grid-template-columns: 1fr 1fr;
			grid-template-rows: 22rem;
		}
	}
</style>
