import { atom } from "nanostores";

export type Tabs = "user" | "cars" | "recommend";

export const $currentTab = atom<Tabs>("user");
export const tabValues: Tabs[] = ["user", "cars", "recommend"];
