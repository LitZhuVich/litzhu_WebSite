<template>
	<el-space wrap size="large">
		<el-card
			:body-style="{ padding: '0px' }"
			v-for="item in props.article"
			:key="item.id"
			@click="articleDesc(item.id)"
			class="item w-90">
			<!-- TODO:图片没搞好 -->
			<img :src="common.url + '/image/' + item.image" class="image w-full h-50 object-cover" />
			<div class="flex flex-col justify-between p-5 relative min-h-50">
				<div>
					<span class="title text-8 font-bold absolute -top-5">{{ item.title }}</span>
					<el-tag type="warning" v-for="tag in item.tags" class="ml-2">
						{{ tag.tagName }}
					</el-tag>
				</div>
				<p class="my-10">
					<el-text>{{ item.desc }}</el-text>
				</p>
				<div class="user_info flex flex-justify-start content-center">
					<!-- TODO:文章用户头像 -->
					<el-avatar class="mr-2" :size="30" src="/image/user_tou.jpg" />
					<el-text><span class="mr-2">用户名</span></el-text>
					<el-text type="info">
						<span class="mr-2">
							发布于：
							{{ new Date(item.creationTime).toLocaleString() }}
						</span>
					</el-text>
				</div>
			</div>
		</el-card>
	</el-space>
</template>

<script setup lang="ts">
	// 公共属性
	import { useCommon } from "../stores/Common";
	const common = useCommon();

	// 获取传来的文章内容
	const props = defineProps(["article"]);

	import { useRouter } from "vue-router";
	const router = useRouter();
	// 跳转文章详情
	const articleDesc = (articleId: string | number) => {
		router.push({ name: "ArticleDesc", params: { articleId: articleId } });
	};
</script>

<style lang="scss" scoped>
	.item {
		border-radius: 1rem;

		.title {
			color: #4169e1;
		}
	}
</style>
