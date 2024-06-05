<template>
  <v-container>
    <v-card>
      <v-row class="align-center">
        <v-col>
          <v-card-title>
            Produtos
          </v-card-title>
        </v-col>
        <v-col class="d-flex justify-end">
          <v-btn class="mr-4" @click="openCreateModal">
            Cadastrar
          </v-btn>
        </v-col>
      </v-row>

      <v-dialog v-model="dialog" max-width="500px">
        <v-card>
          <v-card-title>
            <span class="headline">{{ isEditing ? 'Editar Produto' : 'Cadastrar Produto' }}</span>
          </v-card-title>
          <v-card-text>
            <v-form>
              <v-text-field
                label="Nome do Produto"
                v-model="produto.nome"
              ></v-text-field>
              <v-text-field
                label="Calorias"
                v-model="produto.calorias"
                type="number"
              ></v-text-field>
              <v-text-field
                label="Preço"
                v-model="produto.preco"
                type="number"
              ></v-text-field>
              <v-text-field
                label="Quantidade"
                v-model="produto.quantidade"
                type="number"
              ></v-text-field>
            </v-form>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="blue darken-1" text @click="dialog = false">
              Cancelar
            </v-btn>
            <v-btn color="blue darken-1" text @click="saveProduto">
              Salvar
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>

      <v-data-table
        :headers="headers"
        :items="desserts"
        :items-per-page="5"
        class="elevation-1"
        dark
      >
        <template v-slot:top>
          <v-toolbar flat>
            <v-toolbar-title>Produtos</v-toolbar-title>
            <v-divider class="mx-4" inset vertical></v-divider>
            <v-spacer></v-spacer>
            <v-text-field
              v-model="search"
              append-icon="mdi-magnify"
              label="Pesquisar"
              single-line
              hide-details
            ></v-text-field>
          </v-toolbar>
        </template>
        <template v-slot:item.actions="{ item }">
          <v-icon small class="mr-2" @click="editItem(item)">
            mdi-pencil
          </v-icon>
          <v-icon small @click="deleteItem(item)">
            mdi-delete
          </v-icon>
        </template>
      </v-data-table>
    </v-card>
  </v-container>
</template>

<script>
export default {
  name: 'IndexPage',
  data () {
    const produtos = [
      { nome: 'Ice cream sandwich', calorias: 237, preco: 5, quantidade: 10 },
      { nome: 'Eclair', calorias: 262, preco: 7, quantidade: 5 },
      { nome: 'Cupcake', calorias: 305, preco: 4, quantidade: 8 },
      { nome: 'Gingerbread', calorias: 356, preco: 6, quantidade: 12 },
    ];
    return {
      dialog: false,
      isEditing: false,
      produto: {
        nome: '',
        calorias: '',
        preco: 0,
        quantidade: 0
      },
      search: '',
      headers: [
        { text: 'Nome', align: 'start', sortable: false, value: 'nome' },
        { text: 'Calorias', value: 'calorias' },
        { text: 'Preço', value: 'preco' },
        { text: 'Quantidade', value: 'quantidade' },
        { text: 'Actions', value: 'actions', sortable: false },
      ],
      desserts: produtos,
    }
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
    deleteItem(item) {
      // Lógica para deletar o item
      console.log('Delete item:', item);
    },
    saveProduto() {
      if (this.isEditing) {
        const index = this.desserts.findIndex(dessert => dessert.nome === this.produto.nome);
        if (index !== -1) {
          this.$set(this.desserts, index, this.produto);
        }
      } else {
        this.desserts.push(this.produto);
      }
      this.dialog = false;
      this.produto = { nome: '', calorias: '', preco: 0, quantidade: 0 };
    },
  }
}
</script>

<style scoped>
/* Adicione estilos adicionais aqui, se necessário */
</style>
