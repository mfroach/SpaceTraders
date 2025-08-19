<script lang="ts">
	import '../app.css';
	import { ModeWatcher } from "mode-watcher";
	import favicon from '$lib/assets/favicon.svg';
	let { children } = $props();
</script>

<svelte:head>
	<link rel="icon" href={favicon} />
</svelte:head>

<ModeWatcher />

<header class="bg-gray-800 text-white shadow-md">
  <nav class="container mx-auto flex items-center justify-between p-4">
    
    <a href="/" class="text-2xl font-bold">SpaceTradersFront</a>

    <ul class="flex items-center space-x-8 text-lg">
      <li>
        <a href="/" class="hover:text-cyan-400 transition-colors duration-300">Home</a>
      </li>
      <li>
        <a href="/agent" class="hover:text-cyan-400 transition-colors duration-300">Agent</a>
      </li>
      <li>
        <a href="/systems/X1-DS2/waypoints/A1/market" class="hover:text-cyan-400 transition-colors duration-300">Market test</a>
      </li>
      <li>
        <a href="/systems/X1-DS2/waypoints/A1" class="hover:text-cyan-400 transition-colors duration-300">Waypoint test</a>
      </li>
      <li>
        <a href="/systems/X1-DS2/waypoints/cachemarket/A1" class="hover:text-cyan-400 transition-colors duration-300">Cache test</a>
      </li>
    </ul>

  </nav>
</header>

<main class="p-6">
	{@render children?.()}
</main>