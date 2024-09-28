import React, { useEffect, useState } from "react";
import {
    View,
    Text,
    TouchableOpacity,
    Image,
    Alert,
    ScrollView,
} from "react-native";
import AsyncStorage from "@react-native-async-storage/async-storage";
import { useNavigation } from "@react-navigation/native";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { RootStackParamList } from "@/types/types";
import { styles } from "./styles";
import AntDesign from "@expo/vector-icons/AntDesign";
import MaterialCommunityIcons from "@expo/vector-icons/MaterialCommunityIcons";
import Ionicons from "@expo/vector-icons/Ionicons";

type UserProfileNavigationProp = NativeStackNavigationProp<
    RootStackParamList,
    "UserProfile"
>;

const UserProfile = () => {
    const [email, setEmail] = useState<string | null>(null);
    const navigation = useNavigation<UserProfileNavigationProp>();

    useEffect(() => {
        const fetchEmail = async () => {
            try {
                const storedEmail = await AsyncStorage.getItem("userEmail");
                setEmail(storedEmail);
            } catch (error) {
                console.error("Failed to load email from AsyncStorage", error);
            }
        };

        fetchEmail();
    }, []);

    const handleLogout = async () => {
        try {
            await AsyncStorage.removeItem("userEmail");
            navigation.navigate("Splash"); // Navigate back to the splash screen
        } catch (error) {
            Alert.alert("Error", "Failed to log out");
        }
    };

    return (
        <ScrollView style={styles.container}>
            <View style={styles.headerLeftIconsContainer}>
                <AntDesign
                    name="leftcircleo"
                    size={32}
                    color="black"
                    onPress={() => navigation.goBack()}
                />
            </View>
            <View style={styles.headerRightIconsContainer}>
                <AntDesign
                    style={styles.headerRightIcon}
                    name="calendar"
                    size={24}
                    color="#F8931E"
                />
                <AntDesign
                    style={styles.headerRightIcon}
                    name="edit"
                    size={24}
                    color="#F8931E"
                />
                <MaterialCommunityIcons
                    style={styles.headerRightIcon}
                    name="logout"
                    size={24}
                    color="#F8931E"
                    onPress={handleLogout}
                />
            </View>

            <View style={styles.profileHeader}>
                <Image
                    source={{
                        uri: "https://s3-alpha-sig.figma.com/img/264f/d2aa/89d339442522b3298305e9c5f0567b88?Expires=1728259200&Key-Pair-Id=APKAQ4GOSFWCVNEHN3O4&Signature=fD3JrHmp08MMTXDb9ugfe~kierVQVRpN4k0VEY6MFSLBpSfXYp8Ow-ji0Uv05LICZKaGlxIE1Np3VzWvxvRyf~9Z9bmAapyULMMQ3LYDGpwG3jwCSqlEHp7328JVAdBBJAsroMrc~v1UqvbUTfYULF5bNRwvKaGaEoaPOGe-PzQeRCjyyX1J2N93ePa4wJjIg-NfwH9mvDogkbUqa7ZKrtpoEZV5~-ES-QWKpQWEzOjz-ZpZL17YL2IfLVDATWIgXWj1PCWMX0tJmx3efKjabmWvt6RUdck4Bs8qU6Cdni4w~9y4x8FNpri63b4eDyGA-oKSXzWmhkBUk0a0FmdGMA__",
                    }}
                    style={styles.profileImage}
                />

                {/* User Info */}
                <Text style={styles.name}>Tanas</Text>
                <Text style={styles.genderAge}>Male</Text>
                <Text style={styles.description}>
                    A man 25 years old, have the passion with sports, especially football
                    and badminton. Willing to share and connect with everyone.
                </Text>
            </View>

            {/* Sports Section */}
            <View style={styles.section}>
                <View style={styles.sectionHeader}>
                    <Text style={styles.sectionTitle}>SPORTS</Text>
                    <TouchableOpacity>
                        <Text style={styles.addLink}>Add Sport</Text>
                    </TouchableOpacity>
                </View>

                <View style={styles.sportCard}>
                    <View style={styles.sportInfo}>
                        <Ionicons
                            style={styles.sportIcon}
                            name="football"
                            size={35}
                            color="black"
                        />
                        <Text style={styles.sportName}>Football</Text>
                    </View>
                    <View style={styles.sportDetails}>
                        <View style={styles.ratingContainer}>
                            {/* Self rating */}
                            {[...Array(5)].map((_, index) => (
                                <Image
                                    key={index}
                                    source={{
                                        uri: "https://cdn-icons-png.flaticon.com/512/616/616489.png",
                                    }}
                                    style={styles.starIcon}
                                />
                            ))}
                        </View>
                        <Text style={styles.position}>Position: Forward</Text>
                    </View>
                </View>
            </View>

            {/* Club Section */}
            <View style={styles.section}>
                <View style={styles.sectionHeader}>
                    <Text style={styles.sectionTitle}>CLUB</Text>
                    <TouchableOpacity>
                        <Text style={styles.addLink}>Add Club</Text>
                    </TouchableOpacity>
                </View>

                <View style={styles.clubCard}>
                    <Image
                        source={{
                            uri: "https://s3-alpha-sig.figma.com/img/a57f/2358/7f201ed585301973b901bcaafac36cf3?Expires=1728259200&Key-Pair-Id=APKAQ4GOSFWCVNEHN3O4&Signature=NhxKsVxUY48dy6g8cHwviS~qvuV~cRLj06pqPxDrahzQYxwQ-5V4Jn4i4PaFWhmMhNB1QpRLFF~BjHvyQSEFcilmj1UxMC--8yJizzeLRQXBLWtO69Eq2FQ6wne4zxKRkgArl2M2XOilZ~17S5YVsHQ~DX-E2NQuooPlCJmulNqw7lJcZHNkXPSBs3GB6GFt8BrKA-r64wFA4Gc8qIial2OQ~pk-h~XR1ZgZrw5s2aWNubXUI9ER5-80FhAmxSxFRls8W9Rs5cgpnXe~ZgDLWytEpNgOiuekiFWzwYUuYgRK7Ox6w6pyQ00DQ40FuFeSH6scAMoMZEHCmCseU-LrpQ__",
                        }}
                        style={styles.clubLogo}
                    />
                    <View style={styles.clubInfo}>
                        <Text style={styles.clubName}>SPORTYDI CLUB</Text>
                        <Text style={styles.clubDetails}>500 members • Role: Member</Text>
                    </View>
                </View>
            </View>

            {/* Meet History */}
            <TouchableOpacity>
                <Text style={styles.linkText}>VIEW MEETS HISTORY</Text>
            </TouchableOpacity>

            {/* Feedback */}
            <TouchableOpacity>
                <Text style={styles.feedbackLink}>FEEDBACK FOR ADMIN</Text>
            </TouchableOpacity>
        </ScrollView>
    );
};

export default UserProfile;