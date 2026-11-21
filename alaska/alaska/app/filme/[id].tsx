import { router, useLocalSearchParams } from "expo-router";
import { useEffect, useState } from "react";
import {
  ActivityIndicator,
  Image,
  Platform,
  Pressable,
  ScrollView,
  StyleSheet,
  Text,
  View,
} from "react-native";

// Uses localhost on Web, and your PC's IP on mobile
const API_URL =
  Platform.OS === "web"
    ? "http://localhost:5000/api"
    : "http://192.168.20";

type Movie = {
  id: number;
  titulo: string;
  sinopse?: string;
  duracao: number;
  classificacao: number;
  dataLancamento: string;
  posterUrl: string;
  status: number;
};

export default function MovieDetailsScreen() {
  const { id } = useLocalSearchParams();
  const [movie, setMovie] = useState<Movie | null>(null);
  const [loading, setLoading] = useState(true);

  const diasSemana = ["SEG", "TER", "QUA", "QUI", "SEX", "SAB", "DOM"];
  const [diaSelecionado, setDiaSelecionado] = useState("SEG");

  useEffect(() => {
    if (!id) return;

    fetch(`${API_URL}/Filme`)
      .then((res) => res.json())
      .then((allMovies: Movie[]) => {
        const found = allMovies.find((m) => m.id === Number(id)) || null;
        setMovie(found);
      })
      .catch((err) => console.error("Erro ao buscar filme:", err))
      .finally(() => setLoading(false));
  }, [id]);

  if (loading) {
    return (
      <View style={styles.center}>
        <ActivityIndicator size="large" color="#ffffff" />
      </View>
    );
  }

  if (!movie) {
    return (
      <View style={styles.center}>
        <Text style={styles.errorText}>Filme não encontrado.</Text>
        <Pressable style={styles.backButton} onPress={() => router.back()}>
          <Text style={styles.backButtonText}>Voltar</Text>
        </Pressable>
      </View>
    );
  }

  return (
    <ScrollView contentContainerStyle={styles.container}>
      {/* Poster + Movie Title & Info */}
      <View style={styles.headerRow}>
        <Image
          source={{ uri: movie.posterUrl }}
          style={styles.posterBox}
          resizeMode="cover"
        />

        <View style={styles.infoColumn}>
          <Text style={styles.title}>{movie.titulo}</Text>
          <View style={styles.badgeRow}>
            <View style={styles.badge}>
              <Text style={styles.badgeText}>{movie.duracao} min</Text>
            </View>
            <View style={styles.badge}>
              <Text style={styles.badgeText}>
                {movie.classificacao === 0
                  ? "Livre"
                  : `${movie.classificacao} anos`}
              </Text>
            </View>
          </View>

          {/* Placeholder area for when you build session selection later */}
      <View style={styles.card}>
        <Text style={styles.sectionTitle}>Sessões</Text>

  <View style={styles.timeBox}>
    {diasSemana.map((dia) => {
      const isSelected = dia === diaSelecionado;

      return (
        <Pressable
          key={dia}
          onPress={() => setDiaSelecionado(dia)}
          style={[styles.dayButton, isSelected && styles.dayButtonSelected]}
        >
          <Text
            style={[
              styles.timeText,
              isSelected && styles.timeTextSelected,
            ]}
          >
            {dia}
          </Text>
        </Pressable>
      );
    })}
  </View>
</View>
        </View>
      </View>
    </ScrollView>
  );
}

const styles = StyleSheet.create({
  center: {
    flex: 1,
    backgroundColor: "#20222e",
    justifyContent: "center",
    alignItems: "center",
    padding: 20,
  },
  container: {
    padding: 16,
    backgroundColor: "#20222e",
    flexGrow: 1,
  },
  errorText: {
    color: "#f1f1f3",
    fontSize: 16,
    marginBottom: 16,
  },
  backButton: {
    backgroundColor: "#2b2e3d",
    paddingHorizontal: 20,
    paddingVertical: 10,
    borderRadius: 8,
  },
  backButtonText: {
    color: "#ffffff",
    fontWeight: "bold",
  },
  headerRow: {
    flexDirection: "row",
    gap: 16,
    marginBottom: 20,
  },
  posterBox: {
    width: 130,
    height: 170,
    backgroundColor: "#2b2e3d",
    borderRadius: 8,
  },
  infoColumn: {
    flex: 1,
  },
  title: {
    color: "#f1f1f3",
    fontSize: 22,
    fontWeight: "bold",
    marginBottom: 10,
  },
  badgeRow: {
    flexDirection: "row",
    gap: 8,
    marginBottom: 10,
  },
  badge: {
    backgroundColor: "#2b2e3d",
    paddingHorizontal: 10,
    paddingVertical: 4,
    borderRadius: 6,
    marginBottom: 10,
  },
  badgeText: {
    color: "#f1f1f3",
    fontSize: 13,
    fontWeight: "600",
  },
  timeBox: {
    flexDirection: "row",
    alignItems: "center",
    gap: 16,
  },
  timeText: {
    width: 60,
    backgroundColor: "#20222e",
    color: "#f1f1f3",
    paddingVertical: 6,
    borderRadius: 6,
    fontWeight: "bold",
    fontSize: 13,
    textAlign: "center",
  },
  metaText: {
    color: "#a0a3b1",
    fontSize: 18,
  },
  card: {
    backgroundColor: "#2b2e3d",
    borderRadius: 8,
    padding: 16,
  },
  sectionTitle: {
    color: "#f1f1f3",
    fontSize: 20,
    fontWeight: "bold",
    marginBottom: 16,
  },
});