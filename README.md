# [Unity 2D] 나만의 작은 용사
> Mobile Platform

<br>

# 📘 소개
<div align="center">

  <img src="https://github.com/hhcczz/Idle-Game/assets/101077489/84ceb5ed-053f-45bf-9dad-1d3dc638b050" width="19%" height="20%"/>
  <img src="https://github.com/hhcczz/Idle-Game/assets/101077489/f745a0d2-69ed-4770-8275-a5b22c4e103c" width="19%" height="20%"/>
  <img src="https://github.com/hhcczz/Idle-Game/assets/101077489/1fd88a07-4683-40ef-92b9-0208a55be075" width="19%" height="20%"/>
  <img src="https://github.com/hhcczz/Idle-Game/assets/101077489/3a589a00-d3ac-4fc3-afce-d7430ee658a8" width="19%" height="20%"/>
  <img src="https://github.com/hhcczz/Idle-Game/assets/101077489/7aaf8433-ac93-4c27-8963-664944501fbd" width="19%" height="20%"/>

  < 게임 플레이 사진 >
</div>

+ Unity 2D Idle RPG 게임입니다.

+ C# + Unity를 사용해보고 싶었고 나만의 게임을 제작해보고 싶어서 시작했습니다.

+ 개발기간: 2024.02.01 ~ 2024.06.16 ( 약 4개월 )

+ 형상관리: Github

<br>

# 🛠️ 개발 환경
+ Unity 2021.3.21f1 LTS
+ C#
+ Android

  <br>

# 🧪 사용 기술
| 기술 | 설명 |
|:---:|:---|
| 디자인 패턴 | ● **싱글톤** + **Static** 2가지를 사용하여 Manager 관리 |
| Save | 게임 데이터를 모두 Json으로 변환하여 관리 ( Dictionary 포함 ) |
| Prefabs | 자주 사용되는 객체가 없어서 사용될 때 마다 Prefabs을 이용해 불러옴 |

<br>

# 📖 게임 설명

> Enemy  

| 이름 | 설명 |
|:---:|:---:|
| Difficulty | Easy, Normal, Hard, Extreme |
| Stage | 각 난이도마다 총 18개의 Stage가 존재 |

<br>

> Boss 

| 이름 | 설명 |
|:---:|:---|
| Stage | 각 난이도마다 총 10개의 Stage가 존재 |

<br>

> Item

| 이름 | 설명 |
|:---:|:---|
| Grade | Common, Rare, UnCommon, Artifact, Mythical, Legend |
| Sword | 36개 |
| Accessory | 36개 |
| Warrant | 29개 |
| Transcendence Equipment | 8개 |

<br>

> MobScroll 

| 이름 | 설명 |
|:---:|:---|
| Type | 몹 마다 다른 스크롤 드랍 |
| Acquisition | 몹 처치시 1 ~ 10개 획득 가능 |

<br>

# 🚀 게임 포인트
  - 광산 : PVE 이외에 컨텐츠 추가로 제작하였습니다.
     - 광산에서 얻는 아이템들이 캐릭터 성장에 필수입니다.
     - 광산에서 얻는 효과 대부분은 대기만성입니다.
       
  - 패시브 활용 : 현재 ＂권능＂이라는 타이틀로 제작하였습니다.
    - 거의 모든 시스템에 "권능" 효과가 부여되며, 업그레이드할 수록 부가효과들이 강화됩니다.
    - 성장 한계점을 높여주어서 게임 콘텐츠 소모를 줄이며, 새로운 "권능"을 해금할때마다 맞는 시스템에 보정이 들어가 게임 재미 요소도 올라갑니다.
      - EX) 상점 무료 뽑기, 적 처치 시 8% 확률로 경험치 (2 * N + WarrantLevel[index])획득
      - EX) 공격시 8% 확률로 N번의 추가 공격
      - EX) 공격시 N% 확률로 피해량의 or 공격력의 3배 적용

  - 몹 스크롤 : 몹 별로 각자 다른 Scroll을 드랍합니다.
     - 무작정 앞 스테이지로 넘어가기보다는 하위 몹을 계속 사냥해 플레이어를 강화하면서 점진적 스펙업 가능하게 설계했습니다.
     - 금전적으로 앞지르는 현질러 유저들도 플레이타임을 길게 늘리기 위해 제작하였습니다.
     - Easy ~ Extreme으로 넘어갈때마다 Scroll 드랍 확률이 달라지며, 드랍 개수는 일정합니다 ( 1 ~ 10개 )
       
  - 뽑기 시스템 : 게임 콘텐츠 중 하나인 뽑기 시스템입니다.
    - 연속 뽑기 : 무기, 악세서리 뽑기 시스템
    - 다이아몬드 뽑기 : 다이아몬드 2,000개를 넣어서 최소 1,000개 ~ 250,000개 뽑기 가능
    - 붉은 보석 뽑기 : 붉은 보석 300개를 넣어서 최소 150개 ~ 60,000개 뽑기 가능
      
  - 플레이어 피로감 : PVE에서는 조작을 최소화 하였으며, 광산에서는 조작을 최대화 했습니다.
    - PVE는 플레이어가 따로 조작하지 않아도 게임 플레이에 지장 없게 설계했습니다.
    - 광산 컨텐츠로 방치 이외에 직접 조작하는 콘텐츠를 넣었습니다.
      
    
<br>

# 📄 기술 문서

+ [📘 문서 보기](./%EB%82%98%EB%A7%8C%EC%9D%98%20%EC%9E%91%EC%9D%80%20%EC%9A%A9%EC%82%AC%20%EA%B8%B0%EC%88%A0%20%EB%AC%B8%EC%84%9C.pdf) 
+ [⬇️ 다운로드하기](https://raw.githubusercontent.com/hhcczz/Idle-Game/main/%EB%82%98%EB%A7%8C%EC%9D%98%20%EC%9E%91%EC%9D%80%20%EC%9A%A9%EC%82%AC%20%EA%B8%B0%EC%88%A0%20%EB%AC%B8%EC%84%9C.pdf)

<br>

# ▶️ 소개 영상
+ [소개 영상](https://www.youtube.com/watch?v=_BroSnrOvzk)

