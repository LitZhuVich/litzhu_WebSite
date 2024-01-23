<template>
	<el-carousel :interval="interval" height="auto" trigger="click" class="banner">
		<el-carousel-item v-for="item in 6" :key="item" class="item">
			<img src="/src/assets/user_tou.jpg" class="w-full h-full object-cover" />
			<el-card class="card">
				<template #header>
					<span class="text-8 font-bold">标题</span>
					<el-tag type="warning" v-for="item in 3" class="ml-2">后端 {{ item }}</el-tag>
				</template>
				<el-text type="info">asiodjsadiosajasiod</el-text>
				<template #footer>
					<div class="user_info">
						<el-avatar
							:size="30"
							src="https://cube.elemecdn.com/0/88/03b0d39583f48206768a7534e55bcpng.png" />
						<el-text>用户名</el-text>
						<el-text type="info">发布于：时间</el-text>
					</div>
				</template>
			</el-card>
		</el-carousel-item>
	</el-carousel>
</template>

<script setup lang="ts">
	import { ref } from "vue";
	const interval = ref(500000); // 轮播时间
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
		border-radius: 0.5rem;
		.item {
			height: 350px;
			display: grid;
			grid-template-columns: 1fr 1fr;
			grid-template-rows: 350px;
			.card {
				display: flex;
				flex-direction: column;
				justify-content: space-between;
				.user_info {
					display: flex;
					justify-content: flex-start;
					align-content: center;
					span {
						margin-right: 0.5rem;
					}
				}
			}
		}
	}

	.el-carousel__item h3 {
		color: #475669;
		opacity: 0.75;
		line-height: 200px;
		margin: 0;
		text-align: center;
	}

	.el-carousel__item:nth-child(2n) {
		background-color: #99a9bf;
	}

	.el-carousel__item:nth-child(2n + 1) {
		background-color: #d3dce6;
	}
</style>
