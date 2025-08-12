# [Unity 2D] 나만의 작은 용사
> Mobile Platform

<br>

# 📘 소개
<table align="center">
  <tr>
    <td align="center">
      <img src="https://github.com/hhcczz/Idle-Game/assets/101077489/84ceb5ed-053f-45bf-9dad-1d3dc638b050" width="175px"/>
    </td>
    <td align="center">
      <img src="https://github.com/hhcczz/Idle-Game/assets/101077489/f745a0d2-69ed-4770-8275-a5b22c4e103c" width="175px"/>
    </td>
    <td align="center">
      <img src="https://github.com/hhcczz/Idle-Game/assets/101077489/1fd88a07-4683-40ef-92b9-0208a55be075" width="175px"/>
    </td>
    <td align="center">
      <img src="https://github.com/hhcczz/Idle-Game/assets/101077489/3a589a00-d3ac-4fc3-afce-d7430ee658a8" width="175px"/>
    </td>
    <td align="center">
      <img src="https://github.com/hhcczz/Idle-Game/assets/101077489/7aaf8433-ac93-4c27-8963-664944501fbd" width="175px"/>
    </td>
  </tr>
  <tr>
    <td align="center">전투</td>
    <td align="center">스텟</td>
    <td align="center">광산</td>
    <td align="center">강화</td>
    <td align="center">상점</td>
  </tr>
</table>

+ Unity 2D 기반의 Idle RPG 게임입니다.
  
+ **C#과 Unity**를 활용하여 직접 게임 개발을 경험해보고자 시작한 프로젝트입니다.
  
+ **개발 기간**: 2024.02.01 ~ 2024.06.16 (약 4개월)
  
+ **형상 관리**: GitHub

<br>

# 🛠️ 개발 환경

| 구성 요소 | 내용 |
|:---|:---|
| **엔진** | Unity 2021.3.21f1 (LTS) |
| **언어** | C# |
| **플랫폼** | Android (모바일 빌드 대상) |
| **버전 관리**| GitHub |

  <br>

# 🧪 사용 기술
| 기술 항목 | 설명 |
|:---|:---|
| **디자인 패턴** | 싱글톤(Singleton) + Static 구조를 결합하여 게임 매니저 클래스들을 효율적으로 관리 |
| **저장 방식** | 게임 데이터를 JSON 형태로 저장 및 불러오며, Dictionary 구조도 포함 |
| **프리팹 활용** | 자주 사용되지 않는 객체는 필요할 때마다 Prefab으로 동적 생성하여 메모리 효율 최적화 |

<br>

# 📖 게임 설명

> Enemy  

| 이름 | 설명 |
|:---|:---|
| **Difficulty** | Easy, Normal, Hard, Extreme의 4단계 난이도 제공 |
| **Stage**      | 각 난이도별로 총 18개의 스테이지 구성 |

<br>

> Boss 

| 이름 | 설명 |
|:---|:---|
| **Difficulty** | 총 10종의 보스가 각각 고유한 난이도로 등장 |
| **Stage** | 각 보스의 난이도별로 10개의 스테이지가 구성됨 |

<br>

> Item

| 이름 | 설명 |
|:---|:---|
| Grade | Common, Rare, Uncommon, Artifact, Mythical, Legend |
| Sword | 총 36종 |
| Accessory | 총 36종 |
| Warrant | 총 29종 |
| Transcendence Equipment | 총 8종의 초월 장비 |

<br>

> MobScroll 

| 이름 | 설명 |
|:---|:---|
| **Type** | 몬스터 종류에 따라 서로 다른 스크롤을 드롭 |
| **Acquisition** | 몬스터 처치 시 1 ~ 10개의 스크롤을 획득 가능 |

<br>

# 🚀 게임 주요 시스템 및 설계 포인트

## ⛏️ 광산 시스템 (Mine System)
- PVE 이외의 별도 콘텐츠로 제작된 성장 보조 시스템입니다.
- 광산에서 획득한 자원은 캐릭터 성장에 필수적으로 사용됩니다.
- 대부분의 광산 효과는 장기적으로 누적되는 **대기만성형 설계**를 따릅니다.

---

## 🛡️ 패시브 "권능" 시스템 (Power System)
- 모든 핵심 시스템에 **권능(Power)** 효과를 부여하며, 단계별 업그레이드가 가능합니다.
- 성장 한계치를 확장시켜 콘텐츠 소모 속도를 낮추고, **새로운 권능 해금 시마다 해당 시스템에 보정 효과**가 적용됩니다.
- **예시 효과**
  - 상점 무료 뽑기 횟수 증가
  - 적 처치 시 `8% 확률로 경험치 (2 * N + WarrantLevel[index])` 획득
  - 공격 시 `8% 확률로 N회 추가 공격`
  - 공격 시 `N% 확률로 피해량의 3배 또는 공격력의 3배 적용`

---

## 📜 몬스터 스크롤 (Scroll Drop System)
- 몬스터마다 고유한 **Scroll 아이템**을 드롭하며, 이를 통해 점진적 스펙업이 가능합니다.
- 무작정 상위 스테이지로 넘어가기보다는, 하위 몹 반복 사냥을 통한 성장 전략을 유도합니다.
- **과금 유저와 일반 유저 간의 콘텐츠 소모 균형 조절**을 목적으로 설계됨.
- 난이도(Easy ~ Extreme)에 따라 드롭 확률은 달라지며, **드롭 개수는 고정(1~10개)** 입니다.

---

## 🎰 뽑기 시스템 (Gacha System)
- 게임의 반복 콘텐츠 중 하나로, 다양한 보상 루트를 제공합니다.
- **연속 뽑기**: 무기 및 액세서리 획득 가능
- **다이아몬드 뽑기**: 2,000 다이아 소모 → 최소 1,000 ~ 최대 250,000 다이아 획득
- **붉은 보석 뽑기**: 300개 소모 → 최소 150 ~ 최대 60,000 보석 획득

---

## 🧠 피로도 설계 (Fatigue Balancing)
- **PVE 전투**는 자동 전투 중심으로 설계하여 조작 피로를 최소화했습니다.
- **광산 콘텐츠**는 능동적 조작을 유도하여 수동 플레이 요소를 강화했습니다.
- 피로도 설계를 통해 **방치형 + 조작형** 플레이의 균형을 맞췄습니다.
      
    
<br>

# 📄 기술 문서

+ 📘 [문서 보기](./GameDesign.pdf)  
+ ⬇️ [다운로드하기](https://raw.githubusercontent.com/hhcczz/Idle-Game/main/GameDesign.pdf)

<br>

# ▶️ 소개 영상
+ [소개 영상](https://www.youtube.com/watch?v=_BroSnrOvzk)

