<template>
	<div class="content">
		<div class="banner">
			<Banner></Banner>
		</div>
		<hr class="my-10" />
		<div class="blog">
			<p class="text-10 font-bold">文章</p>
			<el-row :gutter="50">
				<el-col :span="18">
					<el-collapse class="mb-5">
						<el-collapse-item name="1">
							<template #title>
								<span class="text-6">标签</span>
							</template>

							<el-tag type="info" size="large" class="tag mr-4 mb-4" v-for="o in 30" :key="o">
								<span class="text-4">后端</span>
							</el-tag>
						</el-collapse-item>
					</el-collapse>
					<Article :article="article"></Article>
				</el-col>
				<el-col :span="6">
					<div class="sticky top-20">
						<UserInfo></UserInfo>
						<Tags></Tags>
					</div>
				</el-col>
			</el-row>
		</div>
	</div>
</template>

<script setup lang="ts">
	const article = ["a", "basd", "b,a", { a: 1 }];
</script>

<style lang="scss" scoped></style>
