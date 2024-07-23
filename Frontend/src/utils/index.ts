export const getRoute = (role: "user" | "admin", isActive: boolean): string => {
  if (role === "user") {
    return isActive ? "/app" : "/activation";
  }

  return "/dashboard";
};
