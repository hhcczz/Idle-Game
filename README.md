# [Unity 2D] 나만의 작은 용사
> Mobile Platform

<br>

# 1. 개발 목적
- C# 언어를 사용해보고 싶었고 나만의 게임을 제작해보고 싶어서 시작했습니다.
- 예전부터 게임을 제작하는 것은 좋아했지만, 직접 Unity를 사용해서 제작하는 것은 해보지 않아서 제작하려고 합니다.

<br>

# 2. 개발 환경
+ Unity 2021.3.21f1 LTS
+ C#
+ Android

  <br>

# 3. 사용 기술
| 기술 | 설명 |
|:---:|:---|
| 디자인 패턴 | ● **싱글톤** + **Static** 2가지를 사용하여 Manager 관리 |
| Save | 게임 데이터를 모두 Json으로 변환하여 관리 ( Dictionary 포함 ) |
| Prefabs | 자주 사용되는 객체가 없어서 사용될 때 마다 Prefabs을 이용해 불러옴 |

<br>

# 4. 게임 설명

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
| Grade | UnCommon, Common, Rare, ---- 추가해야함 |
| Sword | 36개 |
| Accessory | 36개 |
| Warrant | 29개 |
| Transcendence Equipment | 8개 |

> MobScroll

<br>

# 5. 구현 기능
+ Object :
   - Player
     - 작은 용사
   - Enemy
     - Slime
     - Wolf
     - Golem
     - Mushroom
     - Skeleton
     - Goblin
     - FlyingEye
     - Devil
   - Boss
     - Pigeon
+ 
- 게임 포인트
  - 광산 + Main PVE 조화롭게 잘 사용
  - 패시브 활용 ( 다른 게임들에서는 주로 유물로 사용 중 )
    - 현재 ＂권능＂이라는 타이틀로 제작해뒀음 29개 ( 추후 계속 추가 예정 )
    - 모든 세부적인 요소에 ＂권능＂이라는 패시브가 들어가며, 업그레이드할수록 더욱 캐릭터를 강하게 + 보조해주는 역할
    - 성장 한계점을 열어주어서 게임 콘텐츠 소모를 줄이며, 모든 부분에 추가 보정 능력치가 들어가는 게 가능해서 게임 재미 요소도 올라감
      - EX) 상점 무료 뽑기, 적 처치 시 8% 확률로 경험치 (2 * N + WarrantLevel[index])획득 가능
  - 뽑기의 다양성 ( 지금 Idle-Game 들은 연속 뽑기를 주로 사용)
    - 연속 뽑기도 있지만 다양한 뽑기 시스템 도입 예정 : 카운터, 연속 카운터, 룰렛, 등등
  - Main PVE는 자동으로 사냥할 수 있지만, 광산은 플레이어 터치를 사용해 게임 진행을 하도록 함
    - 최대한 PVE는 편하게 하고 플레이어가 따로 조작하지 않아도 게임 진행에 무리가 없게 설계하였음
    - 조작 피로 + 스킬 사용 등등 직접 개입하는 것들을 빼며, 플레이어가 게임 캐릭터 성장에만 몰두하게 하였음
    
