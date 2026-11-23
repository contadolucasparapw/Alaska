import { router } from "expo-router";
import { useEffect, useState } from "react";
import { Pressable, ScrollView, StyleSheet, Text, View, Image, ImageBackground } from "react-native";

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

const destaques = [
  {
    id: 1,
    titulo: "Assista toda a trilogia Duna nos cinemas!",
    imageUrl:
      "https://ingresso-a.akamaihd.net/b2b/production/uploads/article/image/2142/69ce55c301d24f8ce35d1d2a0f1aa2be.jpg",
  },
  {
    id: 6,
    titulo: "Jogos Vorazes: De volta a arena...",
    imageUrl: "https://ingresso-a.akamaihd.net/b2b/production/uploads/article/image/6618/e5117975a58da609f66ad866c7840d05.jpg",
  },
  {
    id: 7,
    titulo: "Vingadores: Doutor Destino — Pré-venda em breve",
    imageUrl:
      "https://ingresso-a.akamaihd.net/b2b/production/uploads/article/image/6093/98084e9e6766c9c682917fad5145bafd.jpg",
  },
];

export default function FilmesScreen() {
  
  const [filmesList, setFilmesList] = useState<Movie[]>([]);

  useEffect(() => {
    fetch("http://localhost:5000/api/Filme")
      .then((response) => response.json())
      .then((data) => setFilmesList(data))
      .catch((error) => console.error("Erro ao buscar filmes:", error));
  }, []);

  const featuredMovie = filmesList[0];

  const [activeSlide, setActiveSlide] = useState(0);

useEffect(() => {
  const timer = setInterval(() => {
    setActiveSlide((prev) => (prev + 1) % destaques.length);
  }, 4000);

  return () => clearInterval(timer);
}, []);

const currentBanner = destaques[activeSlide] || destaques[0];

  return (
    <ScrollView contentContainerStyle={styles.container}>
      <Text style={styles.headText}>Filmes em Destaque</Text>

      <View style={styles.carouselWrapper}>
        <Pressable
          style={styles.bannerBox}
          onPress={() => router.push(`/filme/${currentBanner.id}`)}
        >
          <ImageBackground
            source={{ uri: currentBanner.imageUrl }}
            style={styles.bannerImage}
            resizeMode="cover"
          >
            <View style={styles.bannerOverlay}>
              <Text numberOfLines={2} style={styles.bannerTitle}>
                {currentBanner.titulo}
              </Text>
            </View>
          </ImageBackground>
        </Pressable>

        {/* Clickable Pagination Dots */}
        <View style={styles.dotsContainer}>
          {destaques.map((slide, index) => (
            <Pressable
              key={slide.id}
              onPress={() => setActiveSlide(index)}
              style={[
                styles.dot,
                index === activeSlide && styles.dotActive,
              ]}
            />
          ))}
        </View>
      </View>

      <Text style={styles.headText}>Em Cartaz</Text>

      { }
      <View style={styles.grid}>
        {filmesList
        .filter((filme) => filme.id < 6)
        .map((filme) => (
          <Pressable
            key={filme.id}
            style={styles.cardWrapper}
            onPress={() => router.push(`/filme/${filme.id}`)}
          >
            <View style={styles.posterBox}>
              <Image
            source={{ uri: filme.posterUrl }}
            style={styles.posterBox}
            resizeMode="cover"
          />
              </View>
            <Text numberOfLines={2} style={styles.title}>
              {filme.titulo}
            </Text>
          </Pressable>
        ))}
      </View>

      <Text style={styles.headText}>Em Breve</Text>

      { }
      <View style={styles.grid}>
        {filmesList
        .filter((filme) => filme.id > 5)
        .map((filme) => (
          <Pressable
            key={filme.id}
            style={styles.cardWrapper}
            onPress={() => router.push(`/filme/${filme.id}`)}
          >
            <View style={styles.posterBox}>
              <Image
            source={{ uri: filme.posterUrl }}
            style={styles.posterBox}
            resizeMode="cover"
          />
              </View>
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
  carouselWrapper: {
    marginBottom: 24,
},
  bannerBox: {  
    width: "100%",
    height: 440,
    borderRadius: 10,
    overflow: "hidden",
    backgroundColor: "#2b2e3d",
  },
  bannerImage: {
    width: "100%",
    height: "100%",
    justifyContent: "flex-end",
  },
  bannerOverlay: {
    backgroundColor: "rgba(32, 34, 46, 0.78)",
    padding: 14,
  },
  bannerTitle: {
    color: "#ffffff",
    fontSize: 18,
    fontWeight: "bold",
  },
  dotsContainer: {
    flexDirection: "row",
    justifyContent: "center",
    alignItems: "center",
    gap: 8,
    marginTop: 10,
  },
  dot: {
    width: 8,
    height: 8,
    borderRadius: 4,
    backgroundColor: "#4b5066",
  },
  dotActive: {
    width: 22,
    backgroundColor: "#3b82f6",
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
    borderColor: "#ffffff",
    borderRadius: 8,
  },
  title: {
    color: "#f1f1f3",
    fontWeight: "bold",
    fontSize: 15,
    marginTop: 8,
  },
});