<script lang="ts">
	import type { PageProps } from './$types';
	import { Button } from '$lib/components/ui/button/index.js';
	import * as Table from '$lib/components/ui/table/index.js';
	let { data }: PageProps = $props();
	const importItems = data.result.marketData.imports;
	const exportItems = data.result.marketData.exports;
	const exchangeItems = data.result.marketData.exchange;
</script>

<Button onclick={() => location.reload()}>Refresh</Button>
<div id="here"></div>
<Table.Root>
	<Table.Caption>Import Items</Table.Caption>
	<Table.Header>
		<Table.Row>
			<Table.Head class="w-[100px]">Symbol</Table.Head>
			<Table.Head>Name</Table.Head>
			<Table.Head>Description</Table.Head>
		</Table.Row>
	</Table.Header>
	<Table.Body>
            {#each importItems as item (item.symbol)}
            <Table.Row>
                <Table.Cell>{item.symbol}</Table.Cell>
                <Table.Cell>{item.name}</Table.Cell>
                <Table.Cell>{item.description}</Table.Cell>
            </Table.Row>
            {/each}
	</Table.Body>
</Table.Root>
