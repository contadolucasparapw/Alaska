import { router } from "expo-router";
import { useEffect, useState } from "react";
import { Pressable, ScrollView, StyleSheet, Text, View } from "react-native";

type Movie = {
  id: number;
  titulo: string;
  sinopse: string;
  duracao: number;
  classificacao: number;
  dataLancamento: string;
  posterUrl: string;
  status: number;
};

export default function FilmesScreen() {
  // 1. Fixed type from Filme[] to Movie[]
  const [filmesList, setFilmesList] = useState<Movie[]>([]);

  useEffect(() => {
    fetch("http://localhost:5000/api/Filme")
      .then((response) => response.json())
      .then((data) => setFilmesList(data))
      .catch((error) => console.error("Erro ao buscar filmes:", error));
  }, []);

  const featuredMovie = filmesList[0];

  return (
    <ScrollView contentContainerStyle={styles.container}>
      <Text style={styles.headText}>Filmes em Destaque</Text>
      { }
      {featuredMovie && (
        <Pressable
          style={styles.bannerBox}
          onPress={() => router.push(`/filme/${featuredMovie.id}`)}>
          <Text numberOfLines={2} style={styles.bannerTitle}>
                {featuredMovie.titulo}
              </Text>
        
        
        </Pressable>
      )}
      <Text style={styles.headText}>Em Cartaz</Text>

      { }
      <View style={styles.grid}>
        {filmesList.map((filme) => (
          <Pressable
            key={filme.id}
            style={styles.cardWrapper}
            onPress={() => router.push(`/filme/${filme.id}`)}
          >
            <View style={styles.posterBox} />
            <Text numberOfLines={2} style={styles.title}>
              {filme.titulo}
            </Text>
          </Pressable>
        ))}
      </View>

      <Text style={styles.headText}>Em Breve</Text>

      { }
      <View style={styles.grid}>
        {filmesList.map((filme) => (
          <Pressable
            key={filme.id}
            style={styles.cardWrapper}
            onPress={() => router.push(`/filme/${filme.id}`)}
          >
            <View style={styles.posterBox} />
            <Text numberOfLines={2} style={styles.title}>
              {filme.titulo}
            </Text>
          </Pressable>
        ))}
      </View>
    </ScrollView>
  );
}

const styles = StyleSheet.create({
  container: {
    padding: 20,
    backgroundColor: "#20222e",
    flexGrow: 1,
  },
  banner: {
    width: "100%",
    marginBottom: 20,
  },
  bannerBox: {
    width: "100%",
    height: 400,
    backgroundColor: "#2b2e3d", 
    borderRadius: 8,
    overflow: "hidden", 
    marginBottom: 75,
  },  
  bannerTitle: {
  color: "#f1f1f3",
  fontWeight: "bold", 
  fontSize: 24,
  margin: 15,
  marginTop: 350,
},
  headText: {
    color: "#ffffff",
    fontSize: 27,
    fontWeight: "bold",
    marginBottom: 30,
  },
  grid: {
    flexDirection: "row",
    flexWrap: "wrap",
    gap: 25,
  },
  cardWrapper: {
    width: 240,
    marginBottom: 100,
  },
  posterBox: {
    width: 240,
    height: 340,
    backgroundColor: "#ffffff",
    borderRadius: 8,
  },
  title: {
    color: "#f1f1f3",
    fontWeight: "bold",
    fontSize: 15,
    marginTop: 8,
  },
});