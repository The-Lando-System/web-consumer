<template>
<div>
  <request-form v-bind:request="request" />
  <button v-on:click="goBack" class="btn btn-outline-secondary">Back</button>
  <button v-on:click="update" style="float:right" class="btn btn-info">Update</button>
</div>
</template>

<script>
import RequestForm from './RequestForm.vue';

export default {
  components: {
    RequestForm
  },
  props: ['request'],
  methods: {
    update: function() {
      event.preventDefault();
      this.$requestSvc.update(this.$http, this.request)
      .then((response) => {
        this.$broadcaster.emit('updatedRequest', response);
      });
    },
    goBack: function() {
      this.$broadcaster.emit('backToList',{});
    }
  }
}
</script>

<style>

</style>