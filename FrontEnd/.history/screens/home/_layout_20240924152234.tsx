import React, { useState } from "react";
import {
  StyleSheet,
  View,
  Text,
  TouchableOpacity,
  Image,
  ScrollView,
} from "react-native";
import DetailHeader from "../../components/DetailHeader";
import { Ionicons } from "@expo/vector-icons";
import { useRoute } from "@react-navigation/native";

const EventDetailScreen = () => {
  const [joined, setJoined] = useState(false);
  const [activeTab, setActiveTab] = useState("details");
  const route = useRoute();
  const handleTabChange = (tab: React.SetStateAction<string>) => {
    setActiveTab(tab);
  };

  const handleJoin = () => {
    setJoined(true);
  };

  const handleCancel = () => {
    setJoined(false);
  };

  const clubData = {
    name: "THE TAM CLUB",
    frequency: "Every day",
    imageUri:
      "https://i.pinimg.com/564x/40/98/2a/40982a8167f0a53dedce3731178f2ef5.jpg",
  };

  return (
    <ScrollView style={styles.container}>
      <DetailHeader
        date="Sat, Jun 8 @ 3:30 PM"
        title="Football training with Coach TanHuynh hghghghghg"
      />
      <View style={styles.clubInfoSection}>
        <Image source={{ uri: clubData.imageUri }} style={styles.clubImage} />
        <View>
          <Text style={styles.clubName}>{clubData.name}</Text>
          <Text style={styles.clubFrequency}>{clubData.frequency}</Text>
        </View>
      </View>

      <View style={styles.hostSection}>
        <Text style={styles.hostTitle}>Host</Text>
        <View style={styles.hostAvatarContainer}>
          <Image
            source={{ uri: clubData.imageUri }}
            style={styles.hostAvatar}
          />
          <View style={styles.placeholderAvatar} />
          <View style={styles.placeholderAvatar} />
          <View style={styles.placeholderAvatar} />
          <View style={styles.placeholderAvatar} />
        </View>
      </View>

      <View style={styles.detailsSection}>
        <View style={styles.row}>
          <Ionicons name="calendar-clear" size={15} style={styles.icon} />
          <Text style={styles.eventDate}>
            Saturday, June 8, 2024 at 3:30 PM
          </Text>
        </View>
        <Text style={styles.eventDuration}>2 hour(s)</Text>
        <TouchableOpacity>
          <Text style={styles.addToCalendar}>Add to calendar</Text>
        </TouchableOpacity>
        <View>
          <View style={styles.row}>
            <Ionicons name="location-outline" size={15} style={styles.icon} />
            <Text style={styles.locationText}>Sân Vận Động Huyện Phú Giáo</Text>
          </View>
          <Text style={styles.locationDetail}>
            7QVR+WH6, TT. Phước Vĩnh, Phú Giáo, Bình Dương
          </Text>
        </View>
        <View style={styles.row}>
          <Ionicons name="close-circle" size={15} style={styles.icon} />
          <Text style={styles.cancellationInfo}>
            Cancellation freeze 4 hours before start
          </Text>
        </View>
      </View>
      <View style={styles.notesSection}>
        <Text style={styles.notesTitle}>Notes</Text>
        <Text style={styles.note}>• Phí tham gia là 100.000 đồng</Text>
        <Text style={styles.note}>
          • Khi tham gia, sẽ được huấn luyện bởi những người có chuyên môn trong
          câu lạc bộ...
        </Text>
        <Text style={styles.note}>• Mọi trường giao lưu, học hỏi...</Text>
      </View>

      {!joined ? (
        <TouchableOpacity style={styles.joinButton} onPress={handleJoin}>
          <Text style={styles.joinButtonText}>Join Meet</Text>
        </TouchableOpacity>
      ) : (
        <View style={styles.joinedContainer}>
          <Text style={styles.joinedText}>Joined</Text>
          <TouchableOpacity style={styles.cancelButton} onPress={handleCancel}>
            <Text style={styles.cancelButtonText}>Cancel</Text>
          </TouchableOpacity>
        </View>
      )}
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#f7f7f7",
    paddingHorizontal: 0,
    marginTop: 0,
  },
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
  icon: {
    marginRight: 5,
    color: "#141415",
  },
  clubInfoSection: {
    flexDirection: "row",
    alignItems: "center",
    padding: 15,
    backgroundColor: "#FFDE59",
    marginTop: -15,
  },
  clubImage: {
    width: 50,
    height: 50,
    borderRadius: 25,
    marginRight: 15,
  },
  clubName: {
    fontSize: 16,
    fontWeight: "bold",
  },
  clubFrequency: {
    fontSize: 14,
    color: "#6c757d",
  },
  hostSection: {
    margin: 15,
  },
  hostTitle: {
    fontSize: 16,
    fontWeight: "bold",
    marginBottom: 10,
  },
  hostAvatarContainer: {
    flexDirection: "row",
  },
  hostAvatar: {
    width: 40,
    height: 40,
    borderRadius: 20,
    marginRight: 10,
  },
  placeholderAvatar: {
    width: 40,
    height: 40,
    borderRadius: 20,
    backgroundColor: "#ddd",
    marginRight: 10,
  },
  detailsSection: {
    margin: 15,
  },
  eventDate: {
    fontSize: 16,
    fontWeight: "bold",
  },
  eventDuration: {
    fontSize: 14,
    color: "#6c757d",
    marginBottom: 2,
  },
  addToCalendar: {
    color: "#007bff",
    fontSize: 14,
    marginBottom: 15,
    fontWeight: "bold",
  },
  locationText: {
    fontSize: 16,
    fontWeight: "bold",
  },
  locationDetail: {
    fontSize: 14,
    color: "#6c757d",
  },
  cancellationInfo: {
    fontSize: 16,
    color: "#141415",
    marginTop: 2,
    fontWeight: "bold",
  },
  notesSection: {
    paddingHorizontal: 15,
    marginTop: -10,
  },
  notesTitle: {
    fontSize: 16,
    fontWeight: "bold",
    marginBottom: 10,
  },
  note: {
    fontSize: 14,
    color: "#6c757d",
    marginBottom: 5,
  },
  joinButton: {
    backgroundColor: "#ffb233",
    borderRadius: 30,
    alignItems: "center",
    marginHorizontal: 90,
    paddingVertical: 10,
  },
  joinButtonText: {
    color: "#fff",
    fontSize: 16,
    fontWeight: "bold",
  },
  joinedContainer: {
    backgroundColor: "#4CAF50",
    borderRadius: 30,
    alignItems: "center",
    marginHorizontal: 90,
    paddingVertical: 10,
  },
  cancelButton: {
    color: "#fff",
    fontSize: 16,
    fontWeight: "bold",
  },
});

export default EventDetailScreen;
