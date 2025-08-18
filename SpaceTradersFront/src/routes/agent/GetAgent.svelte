<script lang="ts">
	import { deserialize } from "$app/forms";

    async function getAgent() {
        const response = await fetch("http://localhost:5247/my/agent", {method: 'GET'});
        const result = await response.json();
        return { result };
    }
</script>

<div>
<span></span>
</div>