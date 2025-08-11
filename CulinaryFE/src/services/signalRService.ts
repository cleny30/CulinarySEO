import * as signalR from "@microsoft/signalr";
import axiosClient from "@/utils/config/axios";

let connection: signalR.HubConnection | null = null;

const createConnection = async (userId: string) => {
    if (connection) {
        return connection;
    }

    const newConnection = new signalR.HubConnectionBuilder()
        .withUrl(`${axiosClient.defaults.baseURL}/hub?userId=${userId}`, {
            withCredentials: true,
        })
        .withAutomaticReconnect()
        .build();

    try {
        await newConnection.start();
        connection = newConnection;
        return newConnection;
    } catch (error) {
        console.error('SignalR Connection failed:', error);
        throw error;
    }
};

const requestNotificationPermission = (userId: string) => {
    // Implement notification permission logic here
    console.log(`Requesting notification permission for user ${userId}`);
};

export const initializeConnection = async (userId: string) => {
    try {
        const conn = await createConnection(userId);
        requestNotificationPermission(userId);
        return conn;
    } catch (error) {
        console.error('Failed to initialize connection:', error);
        setTimeout(() => initializeConnection(userId), 5000);
    }
};

export const useSignalR = () => {
    return {
        initializeConnection,
        connection,
    };
};
