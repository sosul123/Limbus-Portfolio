# 림버스 컴퍼니 스타일 전투 프로토타입

[게임 플레이 영상 링크 : https://youtu.be/drp4IxtMa-o]
[플레이 가능한 빌드 링크 : https://drive.google.com/file/d/1m8GG1tiJgOUXNlcfn9-uVUF574MVabnB/view?usp=drive_link]

## 📝 프로젝트 소개

이 프로젝트는 Project Moon의 게임 '림버스 컴퍼니'의 핵심 전투 시스템에서 영감을 받아, 하루 동안 Unity로 개발한 전투 프로토타입입니다. 코인 토스를 이용한 합산 메커니즘과 동적인 전투 연출 구현을 목표로 했습니다.
프로젝트문 채용 마감 전까지 지속적 업데이트 예정입니다.


## ✨ 주요 기능

-   **동적 다중 코인 시스템**: 스킬 데이터에 따라 코인 개수가 다르게 생성되고, 각 코인 토스 결과가 실시간으로 전투에 반영됩니다.
-   **코루틴 기반의 순차적 전투 연출**: 이동, 코인 토스, 공격, 복귀 등 복잡한 전투 시퀀스를 코루틴으로 제어하여 동시적이고 순차적인 연출을 구현했습니다.
-   **애니메이터를 이용한 상태 관리**: `Idle`, `Walk`, `Attack` 상태를 애니메이터와 파라미터를 통해 관리하여 스크립트와 애니메이션을 분리했습니다.
-   **커스터마이징 가능한 UI 시스템**: 에셋을 활용한 UI 디자인 변경 및 TMP 폰트 에셋 생성을 통해 UI의 시각적 퀄리티를 높였습니다.

## 🎬 게임 플레이 영상

(1~2분 길이의 유튜브 영상을 여기에 임베드하거나 링크합니다)

## 💻 개발 환경

-   **개발 기간**: 2025년 8월 20일 ~ 2025년 8월 21일
-   **엔진**: Unity 6000.0.24f1
-   **언어**: C#
-   **IDE**: Visual Studio 2022
-   **AI**  : gemini 웹버전, copilot 사용

  
## 📜 사용 에셋 및 출처 (Credits & Assets Used)
### 🎨 그래픽 및 이펙트 (Art & Visual Effects)

-   **에셋 이름**: FREE - 2D Pixel Art Male and Female Character - Sidescroller
-   **제작자**: [GamdalfHardcore]
-   **출처**: [https://gandalfhardcore.itch.io/2d-pixel-art-male-and-female-character]

-   **에셋 이름**: [CoinsPixelArtTipe02]
-   **제작자**: [Artist2D3D]
-   **출처**: [[itch.io](https://artist2d3d.itch.io/pixelartcoinspack2dgoldsilverbronzecoinsforgames)]

-   **에셋 이름**: [Fantasy Wooden GUI  Free]
-   **제작자**: [Black Hammer]
-   **출처**: [https://assetstore.unity.com/packages/2d/gui/fantasy-wooden-gui-free-103811?srsltid=AfmBOorxq55bip2YsuzVtNx55My5iTG8hGZCHmExz94dDf3rvvWb72WC]

-   **에셋 이름**: [Cartoon FX Remaster]
-   **제작자**: [Jean Moreno]
-   **출처**: [https://assetstore.unity.com/packages/vfx/particles/cartoon-fx-remaster-free-109565?locale=ko-KR&gclsrc=aw.ds&gad_source=1&gad_campaignid=22894907450&gbraid=0AAAAADdkVOt0o4tfaTOFG6TK-kEVeTKUN&gclid=Cj0KCQjw5JXFBhCrARIsAL1ckPsQrSt59aHSCm3et-ty4AsKLl1b6fuUssgbyRF5GEgCFyP4djzK8woaAmtLEALw_wcB]


