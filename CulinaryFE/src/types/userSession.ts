export interface UserSession {
  userId: string;
  username: string;
  fullName: string;
  phone: string;
  email: string;
  profilePic?: string;
  roleName: string;
}