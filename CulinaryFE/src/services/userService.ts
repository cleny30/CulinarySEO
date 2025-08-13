import axios from "axios";
import { type User } from "@/types/user";

const API_URL = "http://localhost:5000/api/users"; // URL API của bạn

/**
 * Lấy thông tin chi tiết của người dùng bằng ID.
 * @param userId - ID của người dùng cần lấy thông tin.
 * @returns Promise chứa dữ liệu người dùng.
 */
export const getUserById = async (userId: string): Promise<User> => {
  try {
    const response = await axios.get(`${API_URL}/${userId}`);
    return response.data;
  } catch (error) {
    console.error(`Error fetching user with id ${userId}:`, error);
    throw error;
  }
};

/**
 * Cập nhật thông tin người dùng.
 * @param userId - ID của người dùng cần cập nhật.
 * @param userData - Dữ liệu mới của người dùng.
 * @returns Promise chứa dữ liệu người dùng đã được cập nhật.
 */
export const updateUser = async (
  userId: string,
  userData: Partial<User>
): Promise<User> => {
  try {
    const response = await axios.put(`${API_URL}/${userId}`, userData);
    return response.data;
  } catch (error) {
    console.error(`Error updating user with id ${userId}:`, error);
    throw error;
  }
};

/**
 * Lấy danh sách tất cả người dùng.
 * @returns Promise chứa mảng các người dùng.
 */
export const getAllUsers = async (): Promise<User[]> => {
  try {
    const response = await axios.get(API_URL);
    return response.data;
  } catch (error) {
    console.error("Error fetching all users:", error);
    throw error;
  }
};
