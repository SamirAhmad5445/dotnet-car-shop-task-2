const routes = {
  user: "/app",
  admin: "/dashboard",
};

export const getRoute = (accountType: "user" | "admin"): string => {
  return routes[accountType];
};
