<template>
	<el-carousel :interval="interval" trigger="click" class="banner">
		<el-carousel-item
			class="item"
			v-for="item in props.article.slice(0, 5)"
			@click="articleDesc(item.id)"
			:key="item.id">
			<img :src="common.url + '/image/' + item.image" class="w-full h-full object-cover" />
			<el-card class="flex flex-col justify-between">
				<template #header>
					<span class="text-8 font-bold">{{ item.title }}</span>
					<el-tag type="warning" v-for="tag in item.tags" class="ml-2">
						{{ tag.tagName }}
					</el-tag>
				</template>
				<el-text type="info">{{ item.desc }}</el-text>
				<template #footer>
					<div class="flex flex-justify-start content-center">
						<el-avatar :size="30" src="/image/user_tou.jpg" class="mr-2" />
						<el-text><span class="mr-2">用户名</span></el-text>
						<el-text type="info">
							<span class="mr-2">
								发布于：
								{{ new Date(item.creationTime).toLocaleString() }}
							</span>
						</el-text>
					</div>
				</template>
			</el-card>
		</el-carousel-item>
	</el-carousel>
</template>

<script setup lang="ts">
	const props = defineProps(["article"]);
	// 公共属性
	import { useCommon } from "../stores/Common";
	const common = useCommon();

	import { ref } from "vue";
	const interval = ref(5000); // 轮播时间

	import { useRouter } from "vue-router";
	const router = useRouter();
	// 跳转文章详情
	const articleDesc = (articleId: string | number) => {
		router.push({ name: "ArticleDesc", params: { articleId: articleId } });
	};
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
