<template>
	<router-link to="">
		<el-space wrap size="large">
			<el-card :body-style="{ padding: '0px' }" v-for="o in 10">
				<img
					src="https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png"
					class="image" />
				<div class="p-5">
					<span>Yummy hamburger</span>
					<div class="bottom">
						<el-button text class="button">Operating</el-button>
					</div>
				</div>
			</el-card>
		</el-space>
	</router-link>
</template>

<script setup lang="ts"></script>

<style lang="scss" scoped></style>
