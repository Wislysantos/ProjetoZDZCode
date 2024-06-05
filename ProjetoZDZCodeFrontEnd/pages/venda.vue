<template>
  <v-container>
    <!-- Título da Página -->
    <v-row class="align-center">
      <v-col>
        <v-card-title>Venda de Produtos</v-card-title>
      </v-col>
    </v-row>

    <!-- Busca de Produtos -->
    <v-row>
      <v-col>
        <v-text-field
          v-model="search"
          append-icon="mdi-magnify"
          label="Pesquisar Produtos"
          single-line
          hide-details
        ></v-text-field>
      </v-col>
    </v-row>

    <!-- Tabela de Produtos Disponíveis -->
    <v-data-table
      :headers="headers"
      :items="filteredProducts"
      :items-per-page="5"
      class="elevation-1"
      dark
    >
      <template v-slot:item.actions="{ item }">
        <v-icon small class="mr-2" @click="addToCart(item)">
          mdi-plus
        </v-icon>
      </template>
    </v-data-table>

    <!-- Tabela de Produtos Selecionados para Venda -->
    <v-card>
      <v-card-title>Produtos Selecionados para Venda</v-card-title>
      <v-data-table
        :headers="selectedHeaders"
        :items="cart"
        :items-per-page="5"
        class="elevation-1"
        dark
      >
        <template v-slot:item.quantity="{ item }">
          <v-text-field
            v-model.number="item.quantity"
            type="number"
            @input="updateTotalPrice(item)"
            :min="1"
            :max="item.available"
          ></v-text-field>
        </template>
        <template v-slot:item.total="{ item }">
          <span>{{ item.total }}</span>
        </template>
        <template v-slot:item.actions="{ item }">
          <v-icon small class="mr-2" @click="removeFromCart(item)">
            mdi-minus
          </v-icon>
        </template>
      </v-data-table>

      <!-- Preço Total Geral -->
      <v-row class="justify-end">
        <v-col class="d-flex justify-end">
          <v-card-title>
            Preço Total: {{ totalPrice }}
          </v-card-title>
        </v-col>
      </v-row>
    </v-card>

    <!-- Botão para Confirmar Venda -->
    <v-row class="justify-end">
      <v-col class="d-flex justify-end">
        <v-btn class="mr-4" @click="confirmSale">
          Confirmar Venda
        </v-btn>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
export default {
  name: 'VendaPage',
  data() {
    const produtos = [
      { name: 'Ice cream sandwich', calories: 237, price: 5, available: 10 },
      { name: 'Eclair', calories: 262, price: 7, available: 5 },
      { name: 'Cupcake', calories: 305, price: 4, available: 8 },
      { name: 'Gingerbread', calories: 356, price: 6, available: 12 },
    ];
    return {
      search: '',
      cart: [],
      headers: [
        { text: 'Name', align: 'start', sortable: false, value: 'name' },
        { text: 'Calories', value: 'calories' },
        { text: 'Price', value: 'price' },
        { text: 'Available', value: 'available' },
        { text: 'Actions', value: 'actions', sortable: false },
      ],
      selectedHeaders: [
        { text: 'Name', align: 'start', sortable: false, value: 'name' },
        { text: 'Calories', value: 'calories' },
        { text: 'Price', value: 'price' },
        { text: 'Quantity', value: 'quantity' },
        { text: 'Total', value: 'total' },
        { text: 'Actions', value: 'actions', sortable: false },
      ],
      produtos,
    };
  },
  computed: {
    filteredProducts() {
      return this.produtos.filter(product => 
        product.name.toLowerCase().includes(this.search.toLowerCase())
      );
    },
    totalPrice() {
      return this.cart.reduce((sum, item) => sum + item.total, 0);
    },
  },
  methods: {
    addToCart(item) {
      const cartItem = this.cart.find(cartItem => cartItem.name === item.name);
      if (!cartItem) {
        this.cart.push({ ...item, quantity: 1, total: item.price });
      }
    },
    removeFromCart(item) {
      const index = this.cart.indexOf(item);
      if (index !== -1) {
        this.cart.splice(index, 1);
      }
    },
    updateTotalPrice(item) {
      if (item.quantity > item.available) {
        item.quantity = item.available;
      }
      item.total = item.price * item.quantity;
    },
    confirmSale() {
      // Atualizar a quantidade disponível no estoque
      this.cart.forEach(item => {
        const product = this.produtos.find(product => product.name === item.name);
        if (product) {
          product.available -= item.quantity;
        }
      });
      // Limpar o carrinho
      this.cart = [];
    },
  }
};
</script>

<style scoped>
/* Adicione estilos adicionais aqui, se necessário */
</style>
