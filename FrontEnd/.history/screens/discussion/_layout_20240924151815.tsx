import React, { useState } from "react";
import { View, Text, StyleSheet, ScrollView } from "react-native";

const Discussion = () => {
  const [posts, setPosts] = useState([
    {
      author: "Tanas",
      date: "08/05/2024 6:30 PM",
      content:
        "Emotion After playing: So interesting !!!\nThis is the first time I feel good after playing one kind of sport. The coach and teammate also support me a lot. Hope I have it in the next time.",
    },
    {
      author: "Yen Hoang",
      date: "08/06/2024 6:35 PM",
      content:
        "Emotion After playing: So interesting !!!\nTeam bên kia cần tập luyện nhiều hơn nhé",
    },
    {
      author: "Tanas",
      date: "08/05/2024 6:30 PM",
      content:
        "Emotion After playing: So interesting !!!\nThis is the first time I feel good after playing one kind of sport. The coach and teammate also support me a lot. Hope I have it in the next time.",
    },
    {
      author: "Tanas",
      date: "08/05/2024 6:30 PM",
      content:
        "Emotion After playing: So interesting !!!\nThis is the first time I feel good after playing one kind",
    },
  ]);

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Forum</Text>
      <ScrollView style={styles.scroll}>
        {posts.map((post, index) => (
          <View key={index} style={styles.post}>
            <View style={styles.postHeader}>
              <Text style={styles.author}>{post.author}</Text>
              <Text style={styles.date}>{post.date}</Text>
            </View>
            <Text style={styles.content}>{post.content}</Text>
          </View>
        ))}
      </ScrollView>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#fff",
    padding: 16,
  },
  title: {
    fontSize: 24,
    fontWeight: "bold",
    marginBottom: 16,
  },
  scroll: {
    flex: 1,
  },
  post: {
    backgroundColor: "#f5f5f5",
    padding: 16,
    marginBottom: 16,
    borderRadius: 8,
  },
  postHeader: {
    flexDirection: "row",
    justifyContent: "space-between",
    marginBottom: 8,
  },
  author: {
    fontWeight: "bold",
  },
  date: {
    color: "#999",
  },
  content: {
    fontSize: 16,
  },
});

export default Discussion;
