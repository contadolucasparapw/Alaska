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

  const diasSemana = ["SEG", "TER", "QUA", "QUI", "SEX", "SAB", "DOM"];

  const horariosPorDia: Record<string, string[]> = {
    SEG: ["14:00", "19:00", "21:30"],
    TER: ["14:00", "19:00", "21:30"],
    QUA: ["14:00", "19:00", "21:30"],
    QUI: ["14:00", "19:00", "21:30"],
    SEX: ["14:00", "16:30", "19:00", "21:30", "23:30"],
    SAB: ["13:00", "16:00", "19:00", "21:30", "23:30"],
    DOM: ["13:00", "16:00", "19:00", "21:30"],
  };

  const tiposSessao = ["2D Dublado", "2D Legendado", "IMAX 3D"];

  const precosPorTipo: Record<string, number> = {
  "2D Dublado": 30.0,
  "2D Legendado": 32.0,
  "IMAX 3D": 45.0,
};

  export default function MovieDetailsScreen() {
    const { id } = useLocalSearchParams();
    const [movie, setMovie] = useState<Movie | null>(null);
    const [loading, setLoading] = useState(true);

    const [diaSelecionado, setDiaSelecionado] = useState<string>("SEG");
    const [horarioSelecionado, setHorarioSelecionado] = useState<string | null>(null);
    const [tipoSelecionado, setTipoSelecionado] = useState<string | null>(null);

    const horariosDisponiveis = horariosPorDia[diaSelecionado] || [];

    const [inteira, setInteira] = useState<number>(1);
    const [meia, setMeia] = useState<number>(0);

    const precoInteira = precosPorTipo[tiposSessao || ""] ?? 30.0;
    const precoMeia = precoInteira / 2;
    const totalIngressos = inteira + meia;
    const valorTotal = inteira * precoInteira + meia * precoMeia;

    // True only when day, time, AND session type are all chosen
    const podeContinuar = Boolean(
      diaSelecionado && horarioSelecionado && tipoSelecionado
    );

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

    function handleContinuar() {
      if (!movie || !podeContinuar) return;

      // Navigates to your checkout page passing the movie ID and all chosen options
      router.push({
        pathname: "/sessao/[id]",
        params: {
          id: String(movie.id),
          dia: diaSelecionado,
          horario: horarioSelecionado!,
          tipo: tipoSelecionado!,
        },
      });
    }

    function formatBRL(value: number) {
    return `R$ ${value.toFixed(2).replace(".", ",")}`;
  }

  function handleConfirmarCompra() {
    if (totalIngressos === 0) return;

    const resumo =
      `Filme: ${movie?.titulo}\n` +
      `Sessão: ${dia} às ${horario} (${tipo})\n` +
      `Ingressos: ${inteira}x Inteira, ${meia}x Meia\n` +
      `Total: ${formatBRL(valorTotal)}`;

    if (Platform.OS === "web") {
      window.alert(`Compra confirmada!\n\n${resumo}`);
      router.replace("/");
    } else {
      Alert.alert("Compra confirmada!", resumo, [
        { text: "OK", onPress: () => router.replace("/") },
      ]);
    }
  }

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
        {/* Back Button */}
        <Pressable
          style={({ hovered }) => [
            styles.topBackButton,
            hovered && styles.dayButtonHovered,
          ]}
          onPress={() => (router.canGoBack() ? router.back() : router.replace("/"))}
        >
          <Text style={styles.topBackText}>← Voltar</Text>
        </Pressable>

        {/* Poster on Left | Title, Badges, and Sessions Card on Right */}
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

            {/* Sessions Card */}
            <View style={styles.card}>
              {/* 1. Day Selector */}
              <Text style={styles.sectionTitle}>Sessão</Text>
              <View style={styles.timeBox}>
                {diasSemana.map((dia) => {
                  const isSelected = dia === diaSelecionado;

                  return (
                    <Pressable
                      key={dia}
                      onPress={() => {
                        setDiaSelecionado(dia);
                        setHorarioSelecionado(null);
                      }}
                      style={({ hovered, pressed }) => [
                        styles.dayButton,
                        hovered && !isSelected && styles.dayButtonHovered,
                        pressed && { opacity: 0.8 },
                        isSelected && styles.dayButtonSelected,
                      ]}
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

              {/* 2. Time Selector */}
              <Text style={[styles.sectionTitle, { marginTop: 20 }]}>
                Horários ({diaSelecionado})
              </Text>
              <View style={styles.timeBox}>
                {horariosDisponiveis.map((hora) => {
                  const isSelected = hora === horarioSelecionado;

                  return (
                    <Pressable
                      key={hora}
                      onPress={() => setHorarioSelecionado(hora)}
                      style={({ hovered, pressed }) => [
                        styles.dayButton,
                        hovered && !isSelected && styles.dayButtonHovered,
                        pressed && { opacity: 0.8 },
                        isSelected && styles.dayButtonSelected,
                      ]}
                    >
                      <Text
                        style={[
                          styles.timeText,
                          isSelected && styles.timeTextSelected,
                        ]}
                      >
                        {hora}
                      </Text>
                    </Pressable>
                  );
                })}
              </View>

              {/* 3. Session Type Selector */}
              <Text style={[styles.sectionTitle, { marginTop: 20 }]}>
                Tipo de Sessão
              </Text>
              <View style={styles.timeBox}>
                {tiposSessao.map((tipo) => {
                  const isSelected = tipo === tipoSelecionado;

                  return (
                    <Pressable
                      key={tipo}
                      onPress={() => setTipoSelecionado(tipo)}
                      style={({ hovered, pressed }) => [
                        styles.typeButton,
                        hovered && !isSelected && styles.dayButtonHovered,
                        pressed && { opacity: 0.8 },
                        isSelected && styles.dayButtonSelected,
                      ]}
                    >
                      <Text
                        style={[
                          styles.timeText,
                          isSelected && styles.timeTextSelected,
                        ]}
                      >
                        {tipo}
                      </Text>
                    </Pressable>
                  );
                })}
                </View>
              </View>

              <View style={styles.card}>
                <View style={styles.card}>
        <Text style={styles.sectionTitle}>Ingressos</Text>

        {/* Inteira Row */}
        <View style={styles.ticketRow}>
          <View>
            <Text style={styles.ticketLabel}>Inteira</Text>
            <Text style={styles.ticketPrice}>{formatBRL(precoInteira)}</Text>
          </View>

          <View style={styles.counterControls}>
            <Pressable
              onPress={() => setInteira((q) => Math.max(0, q - 1))}
              disabled={inteira === 0}
              style={[styles.counterBtn, inteira === 0 && styles.btnDisabled]}
            >
              <Text style={styles.counterBtnText}>-</Text>
            </Pressable>

            <Text style={styles.counterValue}>{inteira}</Text>

            <Pressable
              onPress={() => setInteira((q) => q + 1)}
              style={styles.counterBtn}
            >
              <Text style={styles.counterBtnText}>+</Text>
            </Pressable>
          </View>
        </View>

        <View style={styles.divider} />

        {/* Meia-Entrada Row */}
        <View style={styles.ticketRow}>
          <View>
            <Text style={styles.ticketLabel}>Meia-Entrada</Text>
            <Text style={styles.ticketPrice}>{formatBRL(precoMeia)}</Text>
          </View>

          <View style={styles.counterControls}>
            <Pressable
              onPress={() => setMeia((q) => Math.max(0, q - 1))}
              disabled={meia === 0}
              style={[styles.counterBtn, meia === 0 && styles.btnDisabled]}
            >
              <Text style={styles.counterBtnText}>-</Text>
            </Pressable>

            <Text style={styles.counterValue}>{meia}</Text>

            <Pressable
              onPress={() => setMeia((q) => q + 1)}
              style={styles.counterBtn}
            >
              <Text style={styles.counterBtnText}>+</Text>
            </Pressable>
          </View>
        </View>
      </View>

      {/* 3. Total & Checkout Button */}
      <View style={styles.totalCard}>
        <View>
          <Text style={styles.metaText}>
            Total ({totalIngressos}{" "}
            {totalIngressos === 1 ? "ingresso" : "ingressos"})
          </Text>
          <Text style={styles.totalValue}>{formatBRL(valorTotal)}</Text>
        </View>
              {/* 4. Continue Button (Unlocks once Time + Type are picked) */}
              <Pressable
                disabled={!podeContinuar}
                onPress={handleContinuar}
                style={({ hovered, pressed }) => [
                  styles.continueButton,
                  !podeContinuar && styles.continueButtonDisabled,
                  podeContinuar && hovered && styles.continueButtonHovered,
                  podeContinuar && pressed && { opacity: 0.85 },
                ]}
              >
                <Text
                  style={[
                    styles.continueButtonText,
                    !podeContinuar && styles.continueButtonTextDisabled,
                  ]}
                >
                  {podeContinuar
                    ? `Continuar (${diaSelecionado} • ${horarioSelecionado} • ${tipoSelecionado})`
                    : "Selecione um horário e tipo de sessão"}
                </Text>
              </Pressable>
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
    topBackButton: {
      alignSelf: "flex-start",
      paddingVertical: 8,
      paddingHorizontal: 12,
      backgroundColor: "#2b2e3d",
      borderRadius: 8,
      marginBottom: 16,
    },
    topBackText: {
      color: "#f1f1f3",
      fontSize: 15,
      fontWeight: "bold",
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
      alignItems: "flex-start",
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
      marginBottom: 14,
    },
    badge: {
      backgroundColor: "#2b2e3d",
      paddingHorizontal: 10,
      paddingVertical: 4,
      borderRadius: 6,
    },
    badgeText: {
      color: "#f1f1f3",
      fontSize: 13,
      fontWeight: "600",
      },
    card: {
      backgroundColor: "#2b2e3d",
      borderRadius: 8,
      padding: 16,
      marginBottom: 20,
      },
    sectionTitle: {
      color: "#f1f1f3",
      fontSize: 18,
      fontWeight: "bold",
      marginBottom: 12,
      },
    timeBox: {
      flexDirection: "row",
      flexWrap: "wrap",
      alignItems: "center",
      gap: 10,
      },
    dayButton: {
      width: 60,
      backgroundColor: "#20222e",
      paddingVertical: 8,
      borderRadius: 6,
      alignItems: "center",
      justifyContent: "center",
      },
    typeButton: {
      backgroundColor: "#20222e",
      paddingVertical: 8,
      paddingHorizontal: 14,
      borderRadius: 6,
      alignItems: "center",
      justifyContent: "center",
      },
    dayButtonHovered: {
      backgroundColor: "#35394b",
      },
    dayButtonSelected: {
      backgroundColor: "#3b82f6",
      },
    timeText: {
      color: "#a0a3b1",
      fontWeight: "bold",
      fontSize: 13,
      textAlign: "center",
      },
    timeTextSelected: {
      color: "#ffffff",
      },
    ticketRow: {
      flexDirection: "row",
      justifyContent: "space-between",
      alignItems: "center",
      paddingVertical: 6,
    },
    ticketLabel: {
      color: "#f1f1f3",
      fontSize: 16,
      fontWeight: "bold",
    },
    ticketPrice: {
      color: "#a0a3b1",
      fontSize: 14,
      marginTop: 2,
    },
    counterControls: {
      flexDirection: "row",
      alignItems: "center",
      gap: 14,
    },
    counterBtn: {
      width: 38,
      height: 38,
      borderRadius: 8,
      backgroundColor: "#3b82f6",
      alignItems: "center",
      justifyContent: "center",
    },
    btnDisabled: {
      backgroundColor: "#20222e",
      opacity: 0.6,
    },
    counterBtnText: {
      color: "#ffffff",
      fontSize: 20,
      fontWeight: "bold",
    },
    counterValue: {
      color: "#ffffff",
      fontSize: 18,
      fontWeight: "bold",
      minWidth: 24,
      textAlign: "center",
    },
    divider: {
      height: 1,
    backgroundColor: "#20222e",
    marginVertical: 12,
  },
  totalCard: {
    backgroundColor: "#2b2e3d",
    borderRadius: 8,
    padding: 16,
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",
  },
  totalValue: {
    color: "#ffffff",
    fontSize: 22,
    fontWeight: "bold",
    marginTop: 2,
  },
    continueButton: {
      marginTop: 24,
      backgroundColor: "#f1f1f3",
      paddingVertical: 14,
      paddingHorizontal: 16,
      borderRadius: 8,
      alignItems: "center",
      justifyContent: "center",
      },
    continueButtonHovered: {
      backgroundColor: "#ffffff",
      },
    continueButtonDisabled: {
      backgroundColor: "#20222e",
      },
    continueButtonText: {
      color: "#20222e",
      fontSize: 15,
      fontWeight: "bold",
      textAlign: "center",
      },
    continueButtonTextDisabled: {
      color: "#6b7280",
      },
      });