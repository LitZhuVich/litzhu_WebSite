<template>
	<div class="m-4xl">
		<div class="banner">
			<Banner></Banner>
		</div>
		<hr class="my-10" />
		<div class="article">
			<p class="text-10 font-bold">文章</p>
			<el-collapse>
				<el-collapse-item name="1">
					<template #title>
						<span class="text-6">标签</span>
					</template>
					<el-tag type="info" size="large" class="mr-2">后端</el-tag>
					<el-tag type="info" size="large" class="mr-2">后端</el-tag>
				</el-collapse-item>
			</el-collapse>
			<Article></Article>
		</div>
	</div>
</template>

<script setup lang="ts">
	import Banner from "../../components/Banner.vue";
	import Article from "../../components/Article.vue";
</script>

<style lang="scss" scoped></style>
