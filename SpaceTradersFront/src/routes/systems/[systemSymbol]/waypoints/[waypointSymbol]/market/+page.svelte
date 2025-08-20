<script lang="ts">
	import type { PageProps } from './$types';
	import { Button } from '$lib/components/ui/button/index.js';
	import * as Table from '$lib/components/ui/table/index.js';
	let { data }: PageProps = $props();
	const importItems = data.result.marketData.imports;
	const exportItems = data.result.marketData.exports;
	const exchangeItems = data.result.marketData.exchange;
	const tradeItems = data.result.marketData.tradeGoods;
</script>

<Button onclick={() => location.reload()}>Refresh page</Button>
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

<Table.Root>
	<Table.Caption>Trade Goods</Table.Caption>
	<Table.Header>
		<Table.Row>
			<Table.Head class="w-[100px]">Symbol</Table.Head>
			<Table.Head>Type</Table.Head>
			<Table.Head>Trade Volume</Table.Head>
			<Table.Head>Supply</Table.Head>
			<Table.Head>Activity</Table.Head>
			<Table.Head>Purchase Price</Table.Head>
			<Table.Head>Sell Price</Table.Head>
		</Table.Row>
	</Table.Header>
	<Table.Body>
		{#each tradeItems as item (item.symbol)}
			<Table.Row>
				<Table.Cell>{item.symbol}</Table.Cell>
				<Table.Cell>{item.type}</Table.Cell>
				<Table.Cell>{item.tradeVolume}</Table.Cell>
				<Table.Cell>{item.supply}</Table.Cell>
				<Table.Cell>{item.activity}</Table.Cell>
				<Table.Cell>{item.purchasePrice}</Table.Cell>
				<Table.Cell>{item.sellPrice}</Table.Cell>
			</Table.Row>
		{/each}
	</Table.Body>
</Table.Root>

<pre>{JSON.stringify(data, null, 2)}</pre>
