<script lang="ts">
    import type { PageProps } from './$types';
    import { Button } from "$lib/components/ui/button/index.js";
    let { data }: PageProps = $props();
    function refresh() {location.reload}
</script>

<Button>Refresh</Button>
<pre>{JSON.stringify(data.result, null, 2)}</pre>