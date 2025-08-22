<script lang="ts">
	import '../app.css';
	import { ModeWatcher } from 'mode-watcher';
	import * as NavigationMenu from '$lib/components/ui/navigation-menu/index.js';
	import favicon from '$lib/assets/favicon.svg';
	const testLinks: { title: string; href: string }[] = [
		{
			title: 'Agent Test',
			href: '/agent'
		},
		{
			title: 'Market test',
			href: '/systems/X1-DS2/waypoints/A1/market'
		},
		{
			title: 'Caching test',
			href: '/systems/X1-DS2/waypoints/cachemarket/A1'
		},
		{
			title: 'Waypoint test',
			href: '/systems/X1-DS2/waypoints/A1'
		}
	];

		const staticTestLinks: { title: string; href: string }[] = [
		{
			title: 'Agent Test',
			href: '/agent'
		},
		{
			title: 'Market test',
			href: '/systems/X1-DS2/waypoints/A1/market'
		},
		{
			title: 'Caching test',
			href: '/systems/X1-DS2/waypoints/cachemarket/A1'
		},
		{
			title: 'Waypoint test',
			href: '/systems/X1-DS2/waypoints/A1'
		}
	];
	type ListItemProps = {
		title: string;
		href: string;
	};
	let { children } = $props();
	</script>

<svelte:head>
	<link rel="icon" href={favicon} />
</svelte:head>

<ModeWatcher />
{#snippet ListItem({ title, href }: ListItemProps)}
	<li>
		<NavigationMenu.Link {href}>
			<div class="text-sm font-medium leading-none">{title}</div>
		</NavigationMenu.Link>
	</li>
{/snippet}
	<NavigationMenu.Root class="relative z-10 flex w-full justify-center">
			<NavigationMenu.List>
				<NavigationMenu.Item>
					<NavigationMenu.Trigger>Tests</NavigationMenu.Trigger>
					<NavigationMenu.Content>
						<ul>
							{#each testLinks as link (link.title)}
								{@render ListItem({
									href: link.href,
									title: link.title
								})}
							{/each}
						</ul>
					</NavigationMenu.Content>
				</NavigationMenu.Item>
				<NavigationMenu.Item>
					<NavigationMenu.Trigger>Static tests</NavigationMenu.Trigger>
					<NavigationMenu.Content class="hover:text-accent-foreground focus-visible:bg-muted focus-visible:text-accent-foreground data-[state=open]:shadow-mini dark:hover:bg-muted dark:data-[state=open]:bg-muted focus-visible:outline-hidden group inline-flex h-8 w-max items-center justify-center rounded-[7px] bg-transparent px-4 py-2 text-sm font-medium transition-colors hover:bg-white disabled:pointer-events-none disabled:opacity-50 data-[state=open]:bg-white">
						<ul>
							{#each staticTestLinks as link (link.title)}
							{@render ListItem({
								href: link.href,
								title: link.title
							})}
							{/each}
						</ul>
					</NavigationMenu.Content>
				</NavigationMenu.Item>
			</NavigationMenu.List>
	</NavigationMenu.Root>

<main class="p-6">
	{@render children?.()}
</main>
