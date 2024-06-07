<template>
  <v-container>
    <!-- Tabela para listar produtos -->
    <v-card>
      <!-- Título da tabela -->
      <v-card-title>Produtos</v-card-title>

      <!-- Botão para abrir o modal de cadastro -->
      <v-row class="align-center">
        <v-col>
          <v-btn @click="openCreateModal">Cadastrar</v-btn>
        </v-col>
      </v-row>

      <!-- Tabela de dados -->
      <v-data-table
        :headers="headers"
        :items="desserts"
        :items-per-page="5"
        class="elevation-1"
        dark
      >
        <!-- Slot para ações de cada item da tabela -->
        <template v-slot:item.actions="{ item }">
          <v-icon small class="mr-2" @click="editItem(item)">mdi-pencil</v-icon>
          <v-icon small @click="deleteItem(item)">mdi-delete</v-icon>
        </template>
      </v-data-table>
    </v-card>

    <!-- Modal para cadastro e edição de produtos -->
    <v-dialog v-model="dialog" max-width="500px">
      <v-card>
        <!-- Título do modal -->
        <v-card-title>{{ isEditing ? 'Editar Produto' : 'Cadastrar Produto' }}</v-card-title>
        
        <!-- Formulário para cadastro e edição -->
        <v-card-text>
          <v-form ref="form">
            <v-text-field label="Nome do Produto" v-model="produto.nome"></v-text-field>
            <v-text-field label="Calorias" v-model.number="produto.calorias" type="number"></v-text-field>
            <v-text-field label="Preço" v-model.number="produto.preco" type="number"></v-text-field>
            <v-text-field label="Quantidade" v-model.number="produto.quantidade" type="number"></v-text-field>
          </v-form>
        </v-card-text>
        
        <!-- Botões de ação -->
        <v-card-actions>
          <v-btn color="blue darken-1" text @click="dialog = false">Cancelar</v-btn>
          <v-btn color="blue darken-1" text @click="saveProduto">Salvar</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-container>
</template>

<script>
export default {
  name: 'IndexPage',
  data() {
    return {
      dialog: false,
      isEditing: false,
      produto: {
        nome: '',
        calorias: '',
        preco: 0,
        quantidade: 0
      },
      headers: [
        { text: 'Nome', align: 'start', sortable: false, value: 'nome' },
        { text: 'Calorias', value: 'calorias' },
        { text: 'Preço', value: 'preco' },
        { text: 'Quantidade', value: 'quantidade' },
        { text: 'Actions', value: 'actions', sortable: false },
      ],
      desserts: [],
    };
  },
  created() {
    this.fetchProdutos();
  },
  methods: {
    openCreateModal() {
      this.isEditing = false;
      this.produto = { nome: '', calorias: '', preco: 0, quantidade: 0 };
      this.dialog = true;
    },
    editItem(item) {
      this.isEditing = true;
      this.produto = { ...item };
      this.dialog = true;
    },
    async deleteItem(item) {
      const index = this.desserts.indexOf(item);
      if (index !== -1) {
        try {
          await fetch(`https://localhost:44366/api/Produtos/${item.id}`, {
            method: 'DELETE',
          });
          this.desserts.splice(index, 1);
        } catch (error) {
          console.error('Erro ao excluir produto:', error);
        }
      }
    },
    async saveProduto() {
      try {
        if (this.isEditing) {
          const response = await fetch(`https://localhost:44366/api/Produtos/${this.produto.id}`, {
            method: 'PUT',
            headers: {
              'Content-Type': 'application/json',
            },
            body: JSON.stringify(this.produto),
          });
          if (response.ok) {
            const index = this.desserts.findIndex(dessert => dessert.id === this.produto.id);
            if (index !== -1) {
              this.$set(this.desserts, index, this.produto);
            }
          }
        } else {
          const response = await fetch('https://localhost:44366/api/Produtos/', {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
            },
            body: JSON.stringify(this.produto),
          });
          if (response.ok) {
            const novoProduto = await response.json();
            console.log(novoProduto)
            this.desserts.push(novoProduto);
          }
        }
        this.dialog = false;
        this.produto = { nome: '', calorias: '', preco: 0, quantidade: 0 };
      } catch (error) {
        console.error('Erro ao salvar produto:', error);
      }
    },
    async fetchProdutos() {
      try {
        const response = await fetch('https://localhost:44366/api/Produtos/');
        const data = await response.json();
        this.desserts = data;
      } catch (error) {
        console.error('Erro ao obter os produtos:', error);
      }
    }
  }
}
</script>

<style scoped>
/* Estilos adicionais aqui */
</style>
