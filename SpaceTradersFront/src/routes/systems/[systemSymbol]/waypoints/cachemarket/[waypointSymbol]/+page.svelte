<script lang="ts">
	import type { PageProps } from '../../../$types';
	import { Button } from '$lib/components/ui/button/index.js';
	import * as Table from '$lib/components/ui/table/index.js';
	let { data }: PageProps = $props();
</script>

<pre>{JSON.stringify(data, null, 2)}</pre>