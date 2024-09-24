import Discussion from "@/screens/discussion/_layout";
import EventDetailScreen from "@/screens/home/_layout";
import React, { useState } from "react";
import { StyleSheet, View, Text, TouchableOpacity } from "react-native";

interface DetailHeaderProps {
  date: string;
  title: string;
}

const DetailHeader: React.FC<DetailHeaderProps> = ({ date, title }) => {
  const [activeTab, setActiveTab] = useState("details");

  const handleTabChange = (newTab: "details" | "discussion") => {
    setActiveTab(newTab);
  };
  return (
    <View style={styles.header}>
      <Text style={styles.headerDate}>{date}</Text>
      <Text style={styles.headerTitle}>{title}</Text>
      <View style={styles.tabContainer}>
        <TouchableOpacity
          style={styles.activeTab}
          onPress={() => handleTabChange("details")}
        >
          <Text style={styles.tabTextActive}>Details</Text>
        </TouchableOpacity>
        <TouchableOpacity
          style={styles.inactiveTab}
          onPress={() => handleTabChange("discussion")}
        >
          <Text style={styles.tabTextInactive}>Discussion</Text>
        </TouchableOpacity>
      </View>

      {activeTab === "details" && <EventDetailScreen />}

      {activeTab === "discussion" && <Discussion />}
    </View>
  );
};

const styles = StyleSheet.create({
  header: {
    backgroundColor: "#FFB41C",
    padding: 20,
    marginBottom: 15,
    alignItems: "center",
    justifyContent: "center",
  },
  headerDate: {
    fontSize: 14,
    color: "#FF915D",
    marginTop: 30,
    fontWeight: "bold",
    textAlign: "center",
  },
  headerTitle: {
    fontSize: 19,
    color: "#fff",
    fontWeight: "bold",
    marginBottom: 15,
    textAlign: "center",
  },
  tabContainer: {
    flexDirection: "row",
    justifyContent: "center",
    alignItems: "center",
    marginLeft: 50,
  },
  activeTab: {
    paddingHorizontal: 15,
    backgroundColor: "#FFD01C",
    paddingVertical: 10,
    borderRadius: 10,
    alignItems: "center",
  },
  inactiveTab: {
    flex: 1,
    backgroundColor: "transparent",
    paddingVertical: 10,
    alignItems: "center",
  },
  tabTextActive: {
    color: "#fff",
    fontWeight: "bold",
    fontSize: 15,
  },
  tabTextInactive: {
    color: "#fff",
    fontWeight: "bold",
    fontSize: 15,
  },
});

export default DetailHeader;
