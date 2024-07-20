import type React from "react";
import useNanoStor from "../hooks/use-nano-store";
import { $currentTab, tabValues } from "../stores/current-tab";
import UserTab from "./tabs/UserTab";
import CarsTab from "./tabs/CarsTab";
import RecommendTab from "./tabs/RecommendTab";

interface DashboardAppProps {
  adminUsername: string;
}

const DashboardApp: React.FC<DashboardAppProps> = ({ adminUsername }) => {
  const [currentTab, setCurrentTab] = useNanoStor($currentTab);

  return (
    <>
      <aside className="flex min-w-72 flex-col gap-1 rounded-lg bg-slate-800 p-2">
        <h2 className="px-2 py-4 text-2xl font-medium text-indigo-200">
          Welcome, {adminUsername}!
        </h2>
        {tabValues.map((tab) => (
          <button
            key={tab}
            data-tab={tab}
            className="rounded-md px-4 py-2 text-start font-medium capitalize [&[data-active='true']]:bg-indigo-800"
            data-active={currentTab === tab}
            onClick={() => setCurrentTab(tab)}
          >
            {tab}
          </button>
        ))}
      </aside>

      <main className="w-full p-4">
        {currentTab === "user" && <UserTab />}
        {currentTab === "cars" && <CarsTab />}
        {currentTab === "recommend" && <RecommendTab />}
      </main>
    </>
  );
};

export default DashboardApp;
