<template>
	<Header></Header>
	<div class="main">
		<router-view name="main"></router-view>
	</div>
</template>

<script setup lang="ts">
	import Header from "./Front/Header.vue";
</script>

<style scoped>
	@media (max-width: 768px) {
		.main {
			margin: 1.5rem 2rem 0 2rem !important;
		}
	}
	.main {
		margin: 1.5rem 8rem 0 8rem;
	}
</style>
