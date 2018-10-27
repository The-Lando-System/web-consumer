<template>
<div class="saved-requests-area">
  <div class="list-group">
    <a href="javascript:void(0);" class="list-group-item list-group-item-action"
       v-for="(request, index) in savedRequests"
       v-bind:key="request.Name"
       v-bind:class="activeButtonIndex === index ? 'active' : ''"
       v-on:click="activeButtonIndex = index; activeRequest = request;">
      {{request.Name}}
    </a>
  </div>
  <button v-on:click="submitSelectedRequest" class="btn btn-primary">Execute</button>
</div>
</template>

<script>
export default {
   data: function() {
    return {
      activeButtonIndex: 0,
      activeRequest: {},
      savedRequests: []
    }
  },
  computed: {
    activeTab: function(index) {
      return {
        'active':  index == this.activeButtonIndex
      }
    }
  },
  mounted: function () {
    this.getSavedRequests();
  },
  methods: {
    getSavedRequests: function() {
      this.$requestSvc.getSavedRequests(this.$http)
      .then(response => {
        this.savedRequests = response;
        if (this.savedRequests && this.savedRequests.length > 0) {
          this.activeRequest = this.savedRequests[0];
        }
      });
    },
    submitSelectedRequest: function() {
      event.preventDefault();
      this.$requestSvc.submitRequestById(this.$http, this.activeRequest.Id)
      .then(response => {
        this.$broadcaster.emit('executedRequest', response);
      });
    }
  }
}
</script>

<style>
.saved-requests-area {
  margin-top: 20px;
}
.saved-requests-area button {
  margin-top: 20px;
}
.list-group {
  max-height: 400px;
  overflow: auto;
}
</style>
