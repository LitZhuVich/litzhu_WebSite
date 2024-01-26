<template>
	<el-space wrap size="large">
		<el-card
			:body-style="{ padding: '0px' }"
			v-for="item in props.article"
			:key="item"
			@click="articleDesc(item)"
			class="item w-90">
			<img
				src="https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png"
				class="image w-full h-50 object-cover" />
			<div class="flex flex-col justify-between p-5">
				<div>
					<span class="title text-8 font-bold">标题</span>
					<el-tag type="warning" v-for="item in 3" class="ml-2">后端 {{ item }}</el-tag>
				</div>
				<p class="desc my-10">asiodjsadiosajasiodasiodjsadiosajasio</p>
				<div class="user_info flex flex-justify-start content-center">
					<el-avatar class="mr-2" :size="30" src="/image/user_tou.jpg" />
					<el-text><span class="mr-2">用户名</span></el-text>
					<el-text type="info"><span class="mr-2">发布于：时间</span></el-text>
				</div>
			</div>
		</el-card>
	</el-space>
</template>

<script setup lang="ts">
	import { useRouter } from "vue-router";
	const router = useRouter();
	const articleDesc = (articleTitle: string | number) => {
		router.push({ name: "ArticleDesc", params: { articleTitle: articleTitle } });
	};

	const props = defineProps(["article"]);
</script>

<style lang="scss" scoped>
	a {
		text-decoration: none;
	}

	.item {
		border-radius: 1rem;

		.title {
			color: #4169e1;
		}
		.desc {
			color: #909399;
		}
	}
</style>
