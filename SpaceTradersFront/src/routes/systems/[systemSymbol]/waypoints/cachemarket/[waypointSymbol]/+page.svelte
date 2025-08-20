<script lang="ts">
	import type { PageProps } from '../../../$types';
	import { Button } from '$lib/components/ui/button/index.js';
	import * as Cache from '$lib/cache.svelte';
	import { page } from '$app/state';
	let { data }: PageProps = $props();
	let cachedData = $derived(Cache.getCachedMarket(1));
	let systemSymbol: string = page.params.systemSymbol!;
	let waypointSymbol: string = page.params.waypointSymbol!;
</script>

<Button onclick={async () => {
	Cache.setCachedMarket(1, await Cache.getMarket(systemSymbol, waypointSymbol))
	}}>Refresh</Button>
<pre>{JSON.stringify(cachedData, null, 2)}</pre>