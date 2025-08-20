<script lang="ts">
	import '../app.css';
	import { ModeWatcher } from 'mode-watcher';
	import * as NavigationMenu from '$lib/components/ui/navigation-menu/index.js';
	import favicon from '$lib/assets/favicon.svg';
	let { children } = $props();
</script>

<svelte:head>
	<link rel="icon" href={favicon} />
</svelte:head>

<ModeWatcher />

<NavigationMenu.Root>
	<NavigationMenu.List>
		<NavigationMenu.Item>
			<NavigationMenu.Trigger>Home</NavigationMenu.Trigger>
			<NavigationMenu.Content>
				<NavigationMenu.Link>Link home</NavigationMenu.Link>
			</NavigationMenu.Content>
		</NavigationMenu.Item>
	</NavigationMenu.List>
</NavigationMenu.Root>

<header class="bg-gray-800 text-white shadow-md">
	<nav class="container mx-auto flex items-center justify-between p-4">
		<a href="/" class="text-2xl font-bold">SpaceTradersFront</a>

		<ul class="flex items-center space-x-8 text-lg">
			<li>
				<a href="/" class="transition-colors duration-300 hover:text-cyan-400">Home</a>
			</li>
			<li>
				<a href="/agent" class="transition-colors duration-300 hover:text-cyan-400">Agent</a>
			</li>
			<li>
				<a
					href="/systems/X1-DS2/waypoints/A1/market"
					class="transition-colors duration-300 hover:text-cyan-400">Market test</a
				>
			</li>
			<li>
				<a
					href="/systems/X1-DS2/waypoints/A1"
					class="transition-colors duration-300 hover:text-cyan-400">Waypoint test</a
				>
			</li>
			<li>
				<a
					href="/systems/X1-DS2/waypoints/cachemarket/A1"
					class="transition-colors duration-300 hover:text-cyan-400">Cache test</a
				>
			</li>
		</ul>
	</nav>
</header>

<main class="p-6">
	{@render children?.()}
</main>
